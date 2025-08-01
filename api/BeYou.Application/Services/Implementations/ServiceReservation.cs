using AutoMapper;
using FluentValidation;
using System.Globalization;
using BeYou.Application.Enums;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;
using BeYou.Utils;
using BeYou.Domain.Enums;

namespace BeYou.Application.Services.Implementations;

public class ServiceReservation(ICoreService<Reservation> coreService, IServiceBranchScheduleBlock serviceBranchScheduleBlock,
                            IServiceBranchHoliday serviceBranchHoliday, IServiceBranchSchedule serviceBranchSchedule,
                            IMapper mapper, IValidator<Reservation> reservationValidator) : IServiceReservation
{
    private readonly string[] ReservationWithBranchAndCustomer = ["BranchIdNavigation", "CustomerIdNavigation"];

    const string dateFormat = "yyyy-MM-dd";

    /// <inheritdoc />
    public async Task<ResponseReservationDto> CreateReservationAsync(RequestReservationDto reservationDTO)
    {
        var reservation = await ValidateReservationAsync(reservationDTO);
        
        reservation.Active = true;

        var result = await coreService.UnitOfWork.Repository<Reservation>().AddAsync(reservation);
        await coreService.UnitOfWork.SaveChangesAsync();

        if (result == null) throw new NotFoundException("Reserva no se ha creado.");

        return mapper.Map<ResponseReservationDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseReservationDto> UpdateReservationAsync(long id, RequestReservationDto reservationDTO)
    {
        if (!await coreService.UnitOfWork.Repository<Reservation>().ExistsAsync(id))
            throw new NotFoundException("Reserva no encontrada.");

        var existing = await coreService.UnitOfWork.Repository<Reservation>().GetByIdAsync(id);

        var reservation = await ValidateReservationAsync(reservationDTO);
        reservation.Id = id;

        reservation.Active = existing.Active;

        var spec = new BaseSpecification<Reservation>(x => x.Id == id);
        var existingReservation = await coreService.UnitOfWork.Repository<Reservation>().FirstOrDefaultAsync(spec);
        reservation.Active = existingReservation?.Active ?? true;

        coreService.UnitOfWork.Repository<Reservation>().Update(reservation);
        await coreService.UnitOfWork.SaveChangesAsync();

        return await FindByIdAsync(id);
    }


    /// <inheritdoc />
    public async Task<ResponseReservationDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<Reservation>(x => x.Id == id);
        var reservation = await coreService.UnitOfWork.Repository<Reservation>().FirstOrDefaultAsync(spec);
        if (reservation == null) throw new NotFoundException("Reserva no encontrada.");

        return mapper.Map<ResponseReservationDto>(reservation);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservationDto>> ListAllAsync()
    {
        var reservations = await coreService.UnitOfWork.Repository<Reservation>().ListAllAsync();

        return mapper.Map<ICollection<ResponseReservationDto>>(reservations);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservationCalendarAgendaDto>> ListAllByBranchAsync(long branchId, DateOnly? startDate, DateOnly? endDate)
    {
        var spec = new BaseSpecification<Reservation>(x => x.BranchId == branchId);
        if (startDate != null && endDate != null)
        {
            spec = new BaseSpecification<Reservation>(x => x.BranchId == branchId && x.Date >= startDate && x.Date <= endDate);
        }
        var list = await coreService.UnitOfWork.Repository<Reservation>().ListAsync(spec, ReservationWithBranchAndCustomer);

        var calendarAgenda = (from a in list
                              select new ResponseReservationCalendarAgendaDto
                              {
                                  Title = $"{a.Id}-{a.CustomerName}",
                                  Start = new DateTime(a.Date.Year, a.Date.Month, a.Date.Day, a.Hour.Hour, a.Hour.Minute, a.Hour.Second, DateTimeKind.Unspecified),
                                  End = new DateTime(a.Date.Year, a.Date.Month, a.Date.Day, a.Hour.Hour + 1, a.Hour.Minute, a.Hour.Second, DateTimeKind.Unspecified)
                              }).ToList();

        if (startDate != null && endDate != null)
        {
            var blocksAgenda = await GetScheduleBlocksAsync(branchId, startDate.Value, endDate.Value);
            var holidays = await GetScheduleHolidaysAsync(branchId, startDate.Value, endDate.Value);

            if (holidays.Any()) blocksAgenda = blocksAgenda.Except(blocksAgenda.Where(m => holidays.Exists(z => z.Start.ToString(dateFormat) == m.Start.ToString(dateFormat))).ToList()).ToList();

            calendarAgenda.AddRange(blocksAgenda);
            calendarAgenda.AddRange(holidays);
        }

        return calendarAgenda;
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservationDto>> ListAllByBranchAsync(long branchId, DateOnly date)
    {
        var spec = new BaseSpecification<Reservation>(x => x.BranchId == branchId && x.Date == date);
        var reservations = await coreService.UnitOfWork.Repository<Reservation>().ListAsync(spec);

        return mapper.Map<ICollection<ResponseReservationDto>>(reservations);
    }

    /// <inheritdoc />
    public async Task<ICollection<TimeOnly>> ScheduleAvailabilityBranchAsync(long branchId, DateOnly date)
    {
        var weekDayName = DateHourManipulation.GetDayWeekCultureCostaRica(date);
        WeekDayApplication weekDay = (WeekDayApplication)Enum.Parse(typeof(WeekDayApplication), weekDayName);

        var branchSchedule = await serviceBranchSchedule.FindByWeekDayAsync(branchId, weekDay);

        var scheduleRange = DateHourManipulation.GetHoursAsync(branchSchedule.Schedule.StartHour, branchSchedule.Schedule.EndHour.AddHours(-1));

        foreach (var item in branchSchedule.BranchScheduleBlocks)
        {
            var scheduleRangeBlocks = DateHourManipulation.GetHoursAsync(item.StartHour, item.EndHour.AddHours(-1));
            scheduleRange = scheduleRange.Except(scheduleRangeBlocks).ToList();
        }

        var reservations = await ListAllByBranchAsync(branchId, date);

        scheduleRange = scheduleRange.Except(reservations.Select(a => a.Hour)).ToList();

        return scheduleRange;
    }

    /// <summary>
    /// Validate reservation
    /// </summary>
    /// <param name="reservationDTO">Reservation request model to be added/updated</param>
    /// <returns>Reservation</returns>
    private async Task<Reservation> ValidateReservationAsync(RequestReservationDto reservationDTO)
    {
        var reservation = mapper.Map<Reservation>(reservationDTO);
        await reservationValidator.ValidateAndThrowAsync(reservation);
        return reservation;
    }

    /// <summary>
    /// Get list of schedule blocks in agenda mode by branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>List of ResponseReservationCalendarAgendaDto</returns>
    private async Task<List<ResponseReservationCalendarAgendaDto>> GetScheduleBlocksAsync(long branchId, DateOnly startDate, DateOnly endDate)
    {
        var blocks = await serviceBranchScheduleBlock.ListAllByBranchAsync(branchId);
        var daysDiference = DateHourManipulation.GetDaysAsync(startDate, endDate);
        var blocksAgenda = from a in blocks
                           from b in daysDiference
                           where b.ToString("dddd", new CultureInfo("es-CR")).Capitalize().Replace("é", "e").Replace("á", "a") == Enum.GetName(typeof(WeekDay), a.BranchSchedule.Schedule.Day)!
                           select new ResponseReservationCalendarAgendaDto
                           {
                               Title = "",
                               Start = new DateTime(b.Year, b.Month, b.Day, a.StartHour.Hour, a.StartHour.Minute, a.StartHour.Second, DateTimeKind.Unspecified),
                               End = new DateTime(b.Year, b.Month, b.Day, a.EndHour.Hour, a.EndHour.Minute, a.EndHour.Second, DateTimeKind.Unspecified),
                               Display = "background",
                               ClassNames = "bg-danger"
                           };
        return blocksAgenda.ToList();
    }

    /// <summary>
    /// Get list of schedule holidays in agenda mode by branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>List of ResponseReservationCalendarAgendaDto</returns>
    private async Task<List<ResponseReservationCalendarAgendaDto>> GetScheduleHolidaysAsync(long branchId, DateOnly startDate, DateOnly endDate)
    {
        var holidays = await serviceBranchHoliday.ListAllByBranchAsync(branchId, startDate, endDate);
        var holidaysAgenda = from a in holidays
                             select new ResponseReservationCalendarAgendaDto
                             {
                                 Title = $"Feriado: {a.Holiday.Name}",
                                 Start = DateTime.ParseExact(a.Date.ToString(dateFormat), dateFormat, CultureInfo.InvariantCulture),
                                 Display = "background",
                                 ClassNames = "bg-warning",
                                 AllDay = true,
                             };

        return holidaysAgenda.ToList();
    }

    public async Task<bool> ExistsReservationAsync(long id) => await coreService.UnitOfWork.Repository<Reservation>().ExistsAsync(id);
}

using AutoMapper;
using FluentValidation;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;

namespace BeYou.Application.Services.Implementations;

public class ServiceSchedule(ICoreService<Schedule> coreService, IMapper mapper,
                            IValidator<Schedule> scheduleValidator) : IServiceSchedule
{
    private readonly string[] ScheduleWithBranchSchedules = ["BranchSchedules"];

    /// <inheritdoc />
    public async Task<ResponseScheduleDto> CreateScheduleAsync(RequestScheduleDto scheduleDto)
    {
        var schedule = await ValidateSchedule(scheduleDto);

        var result = await coreService.UnitOfWork.Repository<Schedule>().AddAsync(schedule);
        await coreService.UnitOfWork.SaveChangesAsync();
        if (result == null) throw new NotFoundException("Horario no se ha creado.");

        return mapper.Map<ResponseScheduleDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseScheduleDto> UpdateScheduleAsync(long id, RequestScheduleDto scheduleDto)
    {
        if (!await coreService.UnitOfWork.Repository<Schedule>().ExistsAsync(id)) throw new NotFoundException("Horario no encontrado.");

        var schedule = await ValidateSchedule(scheduleDto);
        schedule.Id = id;
        schedule.Active = true;

        coreService.UnitOfWork.Repository<Schedule>().Update(schedule);
        await coreService.UnitOfWork.SaveChangesAsync();

        return await FindByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<ResponseScheduleDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<Schedule>(x => x.Id == id);
        var schedule = await coreService.UnitOfWork.Repository<Schedule>().FirstOrDefaultAsync(spec);
        if (schedule == null) throw new NotFoundException("Horario no encontrado.");

        return mapper.Map<ResponseScheduleDto>(schedule);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseScheduleDto>> ListAllAsync()
    {
        var schedules = await coreService.UnitOfWork.Repository<Schedule>().ListAllAsync();

        return mapper.Map<ICollection<ResponseScheduleDto>>(schedules);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteScheduleAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Schedule>().ExistsAsync(id)) throw new NotFoundException("Horario no encontrado.");

        var spec = new BaseSpecification<Schedule>(x => x.Id == id);
        var schedule = await coreService.UnitOfWork.Repository<Schedule>().FirstOrDefaultAsync(spec, ScheduleWithBranchSchedules);
        schedule!.Active = false;

        if (schedule.BranchSchedules.Count > 0) throw new BeYouException("Horario asignado en sucursales.");

        coreService.UnitOfWork.Repository<Schedule>().Update(schedule);
        await coreService.UnitOfWork.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Validates the `RequestScheduleDto` object by mapping it to a `Schedule` object 
    /// and applying the corresponding validation rules.
    /// </summary>
    /// <param name="scheduleDTO">The `RequestScheduleDto` object containing the schedule data to validate.</param>
    /// <returns>Returns the validated `Schedule` object.</returns>
    /// <exception cref="ValidationException">Thrown if the schedule data does not pass validation.</exception>
    private async Task<Schedule> ValidateSchedule(RequestScheduleDto scheduleDTO)
    {
        var schedule = mapper.Map<Schedule>(scheduleDTO);
        await scheduleValidator.ValidateAndThrowAsync(schedule);
        return schedule;
    }
}

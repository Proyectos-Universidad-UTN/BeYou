using AutoMapper;
using FluentValidation;
using BeYou.Infrastructure;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;

namespace BeYou.Application.Services.Implementations;

public class ServiceHoliday(ICoreService<Holiday> coreService, IMapper mapper,
                            IValidator<Holiday> holidayValidator) : IServiceHoliday
{
    /// <inheritdoc />
    public async Task<ResponseHolidayDto> CreateHolidayAsync(RequestHolidayDto holidayDto)
    {
        var holiday = await ValidarHoliday(holidayDto);

        var result = await coreService.UnitOfWork.Repository<Holiday>().AddAsync(holiday);
        await coreService.UnitOfWork.SaveChangesAsync();

        if (result == null) throw new NotFoundException("Feriado no creado.");

        return mapper.Map<ResponseHolidayDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseHolidayDto> UpdateHolidayAsync(long id, RequestHolidayDto holidayDto)
    {
        if (!await coreService.UnitOfWork.Repository<Holiday>().ExistsAsync(id)) throw new NotFoundException("Feriado no encontrado.");

        var holiday = await ValidarHoliday(holidayDto);
        holiday.Id = id;
        holiday.Active = true;
        coreService.UnitOfWork.Repository<Holiday>().Update(holiday);

        await coreService.UnitOfWork.SaveChangesAsync();

        return await FindByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteHolidayAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Holiday>().ExistsAsync(id)) throw new NotFoundException("Feriado no encontrada.");

        var spec = new BaseSpecification<Holiday>(x => x.Id == id);
        var holiday = await coreService.UnitOfWork.Repository<Holiday>().FirstOrDefaultAsync(spec);
        holiday!.Active = false;

        coreService.UnitOfWork.Repository<Holiday>().Update(holiday);
        await coreService.UnitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseHolidayDto> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Holiday>().ExistsAsync(id)) throw new NotFoundException("Feriado no encontrada.");

        var spec = new BaseSpecification<Holiday>(x => x.Id == id);
        var holiday = await coreService.UnitOfWork.Repository<Holiday>().FirstOrDefaultAsync(spec);
        if (holiday == null) throw new NotFoundException("Feriado no encontrado.");

        return mapper.Map<ResponseHolidayDto>(holiday);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseHolidayDto>> ListAllAsync()
    {
        var holidays = await coreService.UnitOfWork.Repository<Holiday>().ListAllAsync();

        return mapper.Map<ICollection<ResponseHolidayDto>>(holidays);
    }

    /// <summary>
    /// Validates the `RequestHolidayDto` object by mapping it to a `Holiday` object 
    /// and applying the corresponding validation rules.
    /// </summary>
    /// <param name="holidayDTO">The `RequestHolidayDto` object containing the holiday data to validate</param>
    /// <returns>RequestHolidayDto</returns>
    private async Task<Holiday> ValidarHoliday(RequestHolidayDto holidayDTO)
    {
        var holiday = mapper.Map<Holiday>(holidayDTO);
        await holidayValidator.ValidateAndThrowAsync(holiday);
        return holiday;
    }
}

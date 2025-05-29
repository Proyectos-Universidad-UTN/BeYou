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

public class ServiceBranchHoliday(ICoreService<BranchHoliday> coreService, IMapper mapper,
                                    IValidator<BranchHoliday> branchHolidayValidator) : IServiceBranchHoliday
{
    /// <inheritdoc />
    public async Task<bool> CreateBranchHolidaysAsync(long branchId, IEnumerable<RequestBranchHolidayDto> branchHolidays)
    {
        var holidays = await ValidateHolidays(branchId, branchHolidays);

        var result = await coreService.UnitOfWork.Repository<BranchHoliday>().AddRangeAsync(holidays.ToList());
        await coreService.UnitOfWork.SaveChangesAsync();
        if (result == null) throw new ListNotAddedException("Error al guardar feriados");

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseBranchHolidayDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<BranchHoliday>(x => x.Id == id);
        var branchHoliday = await coreService.UnitOfWork.Repository<BranchHoliday>().FirstOrDefaultAsync(spec);
        if (branchHoliday == null) throw new NotFoundException("Feriado en sucursal no encontrado.");

        return mapper.Map<ResponseBranchHolidayDto>(branchHoliday);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseBranchHolidayDto>> ListAllByBranchAsync(long branchId, short? year)
    {
        var spec = new BaseSpecification<BranchHoliday>(x => x.BranchId == branchId && x.Date.Year == (year ?? x.Date.Year));
        var holidays = await coreService.UnitOfWork.Repository<BranchHoliday>().ListAsync(spec);

        return mapper.Map<ICollection<ResponseBranchHolidayDto>>(holidays);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseBranchHolidayDto>> ListAllByBranchAsync(long branchId, DateOnly startDate, DateOnly endDate)
    {
        var spec = new BaseSpecification<BranchHoliday>(x => x.BranchId == branchId && x.Date >= startDate && x.Date <= endDate);
        var holidays = await coreService.UnitOfWork.Repository<BranchHoliday>().ListAsync(spec);

        return mapper.Map<ICollection<ResponseBranchHolidayDto>>(holidays);
    }

    /// <inheritdoc />
    private async Task<IEnumerable<BranchHoliday>> ValidateHolidays(long branchId, IEnumerable<RequestBranchHolidayDto> branchHolidays)
    {
        var holidays = mapper.Map<List<BranchHoliday>>(branchHolidays);
        foreach (var item in holidays)
        {
            item.BranchId = branchId;
            await branchHolidayValidator.ValidateAndThrowAsync(item);
        }
        return holidays;
    }
}

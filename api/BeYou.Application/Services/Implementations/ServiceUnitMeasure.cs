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

public class ServiceUnitMeasure(ICoreService<UnitMeasure> coreService, IMapper mapper, IValidator<UnitMeasure> unitMeasureValidator) : IServiceUnitMeasure
{
    /// <inheritdoc />
    public async Task<ResponseUnitMeasureDto> CreateUnitMeasureAsync(RequestUnitMeasureDto requestUnitMeasureDto)
    {
        var unitMeasure = await ValidateUnitMeasureAsync(requestUnitMeasureDto);

        var result = await coreService.UnitOfWork.Repository<UnitMeasure>().AddAsync(unitMeasure);
        await coreService.UnitOfWork.SaveChangesAsync();

        if (result == null) throw new NotFoundException("Unidad de medida no creada.");

        return mapper.Map<ResponseUnitMeasureDto>(unitMeasure);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteUnitMeasureAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<UnitMeasure>().ExistsAsync(id)) throw new NotFoundException("Unidad de medida no encontrada.");

        var spec = new BaseSpecification<UnitMeasure>(x => x.Id == id);
        var unitMeasure = await coreService.UnitOfWork.Repository<UnitMeasure>().FirstOrDefaultAsync(spec);

        unitMeasure!.Active = false;

        coreService.UnitOfWork.Repository<UnitMeasure>().Update(unitMeasure);
        await coreService.UnitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseUnitMeasureDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<UnitMeasure>(x => x.Id == id);
        var unitMeasure = await coreService.UnitOfWork.Repository<UnitMeasure>().FirstOrDefaultAsync(spec) ?? throw new NotFoundException("Unidad de medida no encontrada.");

        return mapper.Map<ResponseUnitMeasureDto>(unitMeasure);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseUnitMeasureDto>> ListAllAsync()
    {
        var unitMeasures = await coreService.UnitOfWork.Repository<UnitMeasure>().ListAllAsync();

        return mapper.Map<ICollection<ResponseUnitMeasureDto>>(unitMeasures);
    }

    /// <inheritdoc />
    public async Task<ResponseUnitMeasureDto> UpdateUnitMeasureAsync(long id, RequestUnitMeasureDto requestUnitMeasureDto)
    {
        if (!await coreService.UnitOfWork.Repository<UnitMeasure>().ExistsAsync(id)) throw new NotFoundException("Unidad de medida no encontrada.");

        var unitMeasure = await ValidateUnitMeasureAsync(requestUnitMeasureDto, id);
        coreService.UnitOfWork.Repository<UnitMeasure>().Update(unitMeasure);
        await coreService.UnitOfWork.SaveChangesAsync();

        return await FindByIdAsync(id);
    }

    /// <summary>
    /// Validates if the unit of measure with the given id exists 
    /// </summary>
    /// <param name="id">Id of the unit of measure to check</param>
    /// <param name="symbol">Symbol of the unit of measure to check</param>
    /// <returns>True if the unit of measure with the given id exists, false otherwise</returns>
    private async Task<bool> ExistsBySymbolAsync(long id, string symbol)
    {
        var spec = new BaseSpecification<UnitMeasure>(x => x.Symbol == symbol && x.Id != id);
        return await coreService.UnitOfWork.Repository<UnitMeasure>().FirstOrDefaultAsync(spec) != null;
    }

    /// <summary>
    /// Validates if the unit of measure request can be parsed to a unit of measure
    /// </summary>
    /// <param name="requestUnitMeasureDto">RequestUnitMeasureDto object with the data to create or update a unit of measure</param>
    /// <param name="id">Id of the unit of measure to update. If 0, it means that the request is for a new unit of measure</param>
    /// <returns>UnitMeasure object if the request is valid</returns>
    private async Task<UnitMeasure> ValidateUnitMeasureAsync(RequestUnitMeasureDto requestUnitMeasureDto, long id = 0)
    {
        var unitMeasure = mapper.Map<UnitMeasure>(requestUnitMeasureDto);
        await unitMeasureValidator.ValidateAndThrowAsync(unitMeasure);

        if (await ExistsBySymbolAsync(id, unitMeasure.Symbol)) throw new BeYouException("Ya existe una unidad de medida con el mismo símbolo.");
        unitMeasure.Id = id;
        unitMeasure.Active = true;

        return unitMeasure;
    }
}

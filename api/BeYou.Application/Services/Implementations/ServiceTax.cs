using AutoMapper;
using FluentValidation;
using BeYou.Infrastructure;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Core.Specifications;

namespace BeYou.Application.Services.Implementations;

public class ServiceTax(ICoreService<Tax> coreService, IMapper mapper, IValidator<Tax> taxValidator) : IServiceTax
{
    /// <inheritdoc />
    public async Task<ResponseTaxDto> CreateTaxAsync(RequestTaxDto requestTaxDto)
    {
        var tax = await ValidateTaxAsync(requestTaxDto);
        var result = await coreService.UnitOfWork.Repository<Tax>().AddAsync(tax);
        await coreService.UnitOfWork.SaveChangesAsync();

        if (result == null) throw new NotFoundException("Impuesto no creado.");

        return mapper.Map<ResponseTaxDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Tax>().ExistsAsync(id)) throw new NotFoundException("Impuesto no encontrado.");

        var spec = new BaseSpecification<Tax>(x => x.Id == id);
        var tax = await coreService.UnitOfWork.Repository<Tax>().FirstOrDefaultAsync(spec);
        tax!.Active = false;

        coreService.UnitOfWork.Repository<Tax>().Update(tax);
        await coreService.UnitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseTaxDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<Tax>(x => x.Id == id);
        var tax = await coreService.UnitOfWork.Repository<Tax>().FirstOrDefaultAsync(spec) ?? throw new NotFoundException("Impuesto no encontrado.");

        return mapper.Map<ResponseTaxDto>(tax);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseTaxDto>> ListAllAsync()
    {
        var taxes = await coreService.UnitOfWork.Repository<Tax>().ListAllAsync();

        return mapper.Map<ICollection<ResponseTaxDto>>(taxes);
    }

    /// <inheritdoc />
    public async Task<ResponseTaxDto> UpdateTaxAsync(long id, RequestTaxDto requestTaxDto)
    {
        if (!await coreService.UnitOfWork.Repository<Tax>().ExistsAsync(requestTaxDto.Id)) throw new NotFoundException("Impuesto no encontrado.");

        var tax = await ValidateTaxAsync(requestTaxDto, id);
        coreService.UnitOfWork.Repository<Tax>().Update(tax);
        await coreService.UnitOfWork.SaveChangesAsync();

        return await FindByIdAsync(tax.Id);
    }

    private async Task<bool> ExistsByRateAsync(long id, decimal rate)
    {
        var spec = new BaseSpecification<Tax>(x => x.Id != id && x.Rate == rate);
        var tax = await coreService.UnitOfWork.Repository<Tax>().FirstOrDefaultAsync(spec);

        return tax != null;
    }

    private async Task<Tax> ValidateTaxAsync(RequestTaxDto requestTaxDto, long id = 0)
    {
        var tax = mapper.Map<Tax>(requestTaxDto);
        await taxValidator.ValidateAndThrowAsync(tax);

        if (await ExistsByRateAsync(id, tax.Rate)) throw new BaseReservationException("Ya existe un impuesto con la misma tasa.");
        tax.Id = id;
        tax.Active = true;

        return tax;
    }
}

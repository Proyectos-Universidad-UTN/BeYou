using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BeYou.Infrastructure;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Application.Configuration.Pagination;

namespace BeYou.Application.Services.Implementations;

public class ServiceVendor(ICoreService<Vendor> coreService, IMapper mapper, IValidator<Vendor> vendorValidator) : IServiceVendor
{
    /// <inheritdoc />
    public async Task<ResponseVendorDto> CreateVendorAsync(RequestVendorDto vendorDto)
    {
        var vendor = await ValidateVendorAsync(vendorDto);

        var result = await coreService.UnitOfWork.Repository<Vendor>().AddAsync(vendor);
        await coreService.UnitOfWork.SaveChangesAsync();

        if (result == null) throw new NotFoundException("Proveedor no creado.");

        return mapper.Map<ResponseVendorDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteVendorAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Vendor>().ExistsAsync(id)) throw new NotFoundException("Proveedor no encontrada.");

        var spec = new BaseSpecification<Vendor>(x => x.Id == id);
        var vendor = await coreService.UnitOfWork.Repository<Vendor>().FirstOrDefaultAsync(spec);
        vendor!.Active = false;

        coreService.UnitOfWork.Repository<Vendor>().Update(vendor);
        await coreService.UnitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseVendorDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<Vendor>(x => x.Id == id);
        var vendor = await coreService.UnitOfWork.Repository<Vendor>().FirstOrDefaultAsync(spec);
        if (vendor == null) throw new NotFoundException("Proveedor no encontrado.");

        return mapper.Map<ResponseVendorDto>(vendor);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseVendorDto>> ListAllAsync()
    {
        var vendors = await coreService.UnitOfWork.Repository<Vendor>().ListAllAsync();

        return mapper.Map<ICollection<ResponseVendorDto>>(vendors);
    }

    /// <inheritdoc />
    public async Task<PagedList<ResponseVendorDto>> ListAllAsync(PaginationParameters paginationParameters)
    {
        var query = coreService.UnitOfWork.Repository<Vendor>().AsQueryable();
        var paginatedCollection = await PagedList<Vendor>.PaginatedCollection(query, paginationParameters.PageNumber, paginationParameters.PageSize);
        var vendors = mapper.Map<ICollection<ResponseVendorDto>>(paginatedCollection);
        var count = await query.CountAsync();

        return PagedList<ResponseVendorDto>.ToPagedList(vendors.ToList(), count, paginationParameters.PageNumber, paginationParameters.PageSize);
    }

    /// <inheritdoc />
    public async Task<ResponseVendorDto> UpdateVendorAsync(long id, RequestVendorDto vendorDto)
    {
        if (!await coreService.UnitOfWork.Repository<Vendor>().ExistsAsync(id)) throw new NotFoundException("Proveedor no encontrada.");

        var vendor = await ValidateVendorAsync(vendorDto);
        vendor.Id = id;

        coreService.UnitOfWork.Repository<Vendor>().Update(vendor);
        await coreService.UnitOfWork.SaveChangesAsync();

        return await FindByIdAsync(id);
    }

    /// <summary>
    /// Validate vendor
    /// </summary>
    /// <param name="vendorDto">Vendor request model to be added/updated</param>
    /// <returns>Vendor</returns>
    private async Task<Vendor> ValidateVendorAsync(RequestVendorDto vendorDto)
    {
        var vendor = mapper.Map<Vendor>(vendorDto);
        await vendorValidator.ValidateAndThrowAsync(vendor);
        return vendor;
    }
}

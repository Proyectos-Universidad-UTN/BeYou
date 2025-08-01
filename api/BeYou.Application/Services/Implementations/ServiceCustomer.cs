using AutoMapper;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Validations;
using FluentValidation;

namespace BeYou.Application.Services.Implementations;

public class ServiceCustomer(ICoreService<Customer> coreService, IMapper mapper, IValidator<Customer> customerValidator) : IServiceCustomer
{

    /// <inheritdoc />
    public async Task<ResponseCustomerDto> CreateCustomerAsync(RequestCustomerDto customerDto)
    {
        var customer = await ValidateCustomerAsync(customerDto);

        var result = await coreService.UnitOfWork.Repository<Customer>().AddAsync(customer);
        await coreService.UnitOfWork.SaveChangesAsync();

        if (result == null) throw new NotFoundException("Cliente no ha sido creado.");

        return mapper.Map<ResponseCustomerDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseCustomerDto> UpdateCustomerAsync(long id, RequestCustomerDto customerDto)
    {
        if (!await coreService.UnitOfWork.Repository<Customer>().ExistsAsync(id))
            throw new NotFoundException("Cliente no encontrado.");

        var spec = new BaseSpecification<Customer>(x => x.Id == id);
        var existingCustomer = await coreService.UnitOfWork.Repository<Customer>().FirstOrDefaultAsync(spec);

        if (existingCustomer == null)
            throw new NotFoundException("Cliente no ha sido encontrado.");

        var updatedCustomer = mapper.Map<Customer>(customerDto);
        updatedCustomer.Id = id;
        updatedCustomer.Active = existingCustomer.Active;
        updatedCustomer.Created = existingCustomer.Created;
        updatedCustomer.CreatedBy = existingCustomer.CreatedBy;

        coreService.UnitOfWork.Repository<Customer>().Update(updatedCustomer);
        await coreService.UnitOfWork.SaveChangesAsync();

        return await FindByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteCustomerAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Customer>().ExistsAsync(id)) throw new NotFoundException("Cliente no encontrado.");

        var spec = new BaseSpecification<Customer>(x => x.Id == id);
        var customer = await coreService.UnitOfWork.Repository<Customer>().FirstOrDefaultAsync(spec);
        customer!.Active = false;

        coreService.UnitOfWork.Repository<Customer>().Update(customer);
        await coreService.UnitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseCustomerDto?> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Customer>().ExistsAsync(id)) throw new NotFoundException("Cliente no encontrado.");

        var spec = new BaseSpecification<Customer>(x => x.Id == id);
        var customer = await coreService.UnitOfWork.Repository<Customer>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseCustomerDto>(customer);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseCustomerDto>> ListAllAsync()
    {
        var customers = await coreService.UnitOfWork.Repository<Customer>().ListAllAsync();
        return mapper.Map<ICollection<ResponseCustomerDto>>(customers);
    }

    /// <summary>
    /// Validate customer
    /// </summary>
    /// <param name="customerDto">customer request model to be added/updated</param>
    /// <returns>customer</returns>
    private async Task<Customer> ValidateCustomerAsync(RequestCustomerDto customerDto)
    {
        var customer = mapper.Map<Customer>(customerDto);
        await customerValidator.ValidateAndThrowAsync(customer);
        return customer;
    }
}

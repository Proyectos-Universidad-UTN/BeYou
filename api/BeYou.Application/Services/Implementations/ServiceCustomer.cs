using AutoMapper;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;

namespace BeYou.Application.Services.Implementations;

public class ServiceCustomer(ICoreService<Customer> coreService, IMapper mapper) : IServiceCustomer
{
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
}

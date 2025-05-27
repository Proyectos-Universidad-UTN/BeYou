using AutoMapper;
using KeyedSemaphores;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;

namespace BeYou.Application.Services.Implementations;

public class ServiceInvoice(ICoreService<Inventory> coreService, IServiceOrder serviceOrder,
                            IMapper mapper, IValidator<Invoice> invoiceValidator) : IServiceInvoice
{
    /// <inheritdoc />
    public async Task<ResponseInvoiceDto> CreateInvoiceAsync(RequestInvoiceDto invoiceDto)
    {
        var invoice = await ValidateInvoice(invoiceDto);
        ResponseInvoiceDto result = null!;

        ResponseOrderDto? pedido = null;
        if (invoiceDto.OrderId != null && await serviceOrder.ExistsOrderAsync(invoiceDto.OrderId.Value))
        {
            pedido = await serviceOrder.FindByIdAsync(invoiceDto.OrderId.Value);
            pedido.StatusOrderId = 'A'; // TODO: change this
            invoice.BranchId = pedido.BranchId;
        }

        using var keyedSemaphore = await KeyedSemaphore.LockAsync($"CreateInvoice-{invoiceDto.BranchId}");
        var executionStrategy = coreService.UnitOfWork.CreateExecutionStrategy();
        await executionStrategy.ExecuteAsync(async () =>
        {
            using var transaction = await coreService.UnitOfWork.BeginTransactionAsync();
            try
            {
                var result = await coreService.UnitOfWork.Repository<Invoice>().AddAsync(invoice);
                if (result == null) throw new NotFoundException("Factura no creada.");

                if (pedido != null)
                {
                    var orderMapped = mapper.Map<Order>(pedido);
                    coreService.UnitOfWork.Repository<Order>().Update(orderMapped);
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        });

        return mapper.Map<ResponseInvoiceDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseInvoiceDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<Invoice>(x => x.Id == id);
        var invoice = await coreService.UnitOfWork.Repository<Invoice>().FirstOrDefaultAsync(spec);
        if (invoice == null) throw new NotFoundException("Factura no encontrada.");

        return mapper.Map<ResponseInvoiceDto>(invoice);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInvoiceDto>> ListAllAsync()
    {
        var invoices = await coreService.UnitOfWork.Repository<Invoice>().ListAllAsync();
        invoices = invoices.OrderByDescending(x => x.Date).ToList();
        var collection = mapper.Map<ICollection<ResponseInvoiceDto>>(invoices);

        return collection;
    }

    /// <summary>
    /// Validates the provided Invoice data and returns a mapped Invoice entity.
    /// </summary>
    /// <param name="invoiceDto">The data transfer object containing the Invoice information to validate.</param>
    /// <returns>RequestInvoiceDto</returns>
    private async Task<Invoice> ValidateInvoice(RequestInvoiceDto invoiceDto)
    {
        var invoice = mapper.Map<Invoice>(invoiceDto);
        await invoiceValidator.ValidateAndThrowAsync(invoice);
        return invoice;
    }
}

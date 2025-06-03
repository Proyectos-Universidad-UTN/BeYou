using AutoMapper;
using FluentValidation;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;
using BeYou.Domain.Enums;

namespace BeYou.Application.Services.Implementations;

public class ServiceInventoryProductTransaction(ICoreService<InventoryProductTransaction> coreService, IServiceInventoryProduct serviceInventoryProduct,
                                                IMapper mapper, IValidator<InventoryProductTransaction> inventoryProductTransactionValidator) : IServiceInventoryProductTransaction
{
    /// <inheritdoc />
    public async Task<bool> CreateInventoryProductTransactionAsync(RequestInventoryProductTransactionDto inventoryProductTransactionDto)
    {
        var inventoryProductTransaction = await ValidateInventoryProductTransactionAsync(inventoryProductTransactionDto);

        var inventoryProduct = await serviceInventoryProduct.FindByIdAsync(inventoryProductTransaction.InventoryProductId);
        if (inventoryProduct == null) throw new NotFoundException("Inventario producto no creado.");

        if (inventoryProductTransaction.TransactionType == TransactionTypeInventory.Out && inventoryProduct.Assignable - inventoryProductTransaction.Quantity < 0)
            throw new BaseReservationException("No puede generar un movimiento de inventario con una cantidad mayor a la disponible.");

        var newAssignableQuantity = inventoryProductTransactionDto.TransactionType == Enums.TransactionTypeInventoryApplication.Entrada ?
                            inventoryProductTransaction.Quantity : inventoryProductTransaction.Quantity * -1 + inventoryProduct.Assignable;

        if (newAssignableQuantity > inventoryProduct.Maximum)
            throw new BaseReservationException("Cantidad nueva disponible excede el máximo asignado.");

        if (newAssignableQuantity < inventoryProduct.Minimum)
            throw new BaseReservationException("Cantidad nueva disponible es menor al mínimo asignado.");

        var result = await coreService.UnitOfWork.Repository<InventoryProductTransaction>().AddAsync(inventoryProductTransaction);
        await coreService.UnitOfWork.SaveChangesAsync();

        if (result == null) throw new NotFoundException("Movimiento inventario no creado.");

        return true;
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInventoryProductTransactionDto>> ListAllByInventoryAsync(long inventoryId)
    {
        var spec = new BaseSpecification<InventoryProductTransaction>(x => x.InventoryProduct.InventoryId == inventoryId);
        var transactions = await coreService.UnitOfWork.Repository<InventoryProductTransaction>().ListAsync(spec);

        return mapper.Map<ICollection<ResponseInventoryProductTransactionDto>>(transactions);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInventoryProductTransactionDto>> ListAllByProductAsync(long productId)
    {
        var spec = new BaseSpecification<InventoryProductTransaction>(x => x.InventoryProduct.ProductId == productId);
        var transactions = await coreService.UnitOfWork.Repository<InventoryProductTransaction>().ListAsync(spec);

        return mapper.Map<ICollection<ResponseInventoryProductTransactionDto>>(transactions);
    }

    /// <summary>
    /// Validate inventory product movement
    /// </summary>
    /// <param name="inventoryProductTransactionDto">Inventory product movement request model to be added</param>
    /// <returns>InventoryProductTransaction</returns>
    private async Task<InventoryProductTransaction> ValidateInventoryProductTransactionAsync(RequestInventoryProductTransactionDto inventoryProductTransactionDto)
    {
        var inventoryProductTransaction = mapper.Map<InventoryProductTransaction>(inventoryProductTransactionDto);
        await inventoryProductTransactionValidator.ValidateAndThrowAsync(inventoryProductTransaction);
        return inventoryProductTransaction;
    }
}

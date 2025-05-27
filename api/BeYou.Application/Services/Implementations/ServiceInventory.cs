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

public class ServiceInventory(ICoreService<Inventory> coreService, IMapper mapper, IValidator<Inventory> inventoryValidator) : IServiceInventory
{
    /// <inheritdoc />
    public async Task<ResponseInventoryDto> CreateInventoryAsync(long branchId, RequestInventoryDto inventoryDto)
    {
        var inventory = await ValidateInventoryAsync(inventoryDto);
        inventory.BranchId = branchId;

        var result = await coreService.UnitOfWork.Repository<Inventory>().AddAsync(inventory);
        await coreService.UnitOfWork.SaveChangesAsync();

        if (result == null) throw new NotFoundException("Inventario no creado.");

        return mapper.Map<ResponseInventoryDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteInventoryAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Inventory>().ExistsAsync(id)) throw new NotFoundException("Inventario no encontrado.");

        var spec = new BaseSpecification<Inventory>(x => x.Id == id);
        var inventory = await coreService.UnitOfWork.Repository<Inventory>().FirstOrDefaultAsync(spec);

        if (inventory!.InventoryProducts.Any(m => m.Assignable != 0)) throw new BaseReservationException("No puede eliminar un inventario con productos disponibles, asegurese que todos los productos tengan cantidad 0 antes de eliminar el inventario");

        inventory.Active = false;
        coreService.UnitOfWork.Repository<Inventory>().Update(inventory);
        await coreService.UnitOfWork.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseInventoryDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<Inventory>(x => x.Id == id);
        var inventory = await coreService.UnitOfWork.Repository<Inventory>().FirstOrDefaultAsync(spec);
        if (inventory == null) throw new NotFoundException("Inventario no encontrado.");

        return mapper.Map<ResponseInventoryDto>(inventory);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInventoryDto>> ListAllAsync()
    {
        var inventories = await coreService.UnitOfWork.Repository<Inventory>().ListAllAsync();

        return mapper.Map<ICollection<ResponseInventoryDto>>(inventories);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInventoryDto>> ListAllByBranchAsync(long branchId)
    {
        var spec = new BaseSpecification<Inventory>(x => x.BranchId == branchId);
        var inventories = await coreService.UnitOfWork.Repository<Inventory>().ListAsync(spec);

        return mapper.Map<ICollection<ResponseInventoryDto>>(inventories);
    }

    /// <inheritdoc />
    public async Task<ResponseInventoryDto> UpdateInventoryAsync(long branchId, long id, RequestInventoryDto inventoryDto)
    {
        if (!await coreService.UnitOfWork.Repository<Inventory>().ExistsAsync(id)) throw new NotFoundException("Inventario no encontrada.");

        var inventory = await ValidateInventoryAsync(inventoryDto);
        inventory.BranchId = branchId;
        inventory.Id = id;

        coreService.UnitOfWork.Repository<Inventory>().Update(inventory);
        await coreService.UnitOfWork.SaveChangesAsync();

        return await FindByIdAsync(id);
    }

    /// <summary>
    /// Validate if inventary could be Mapped to be added/updated
    /// </summary>
    /// <param name="inventoryDto">Inventory model to be validated</param>
    /// <returns>Inventory</returns>
    private async Task<Inventory> ValidateInventoryAsync(RequestInventoryDto inventoryDto)
    {
        var inventory = mapper.Map<Inventory>(inventoryDto);
        await inventoryValidator.ValidateAndThrowAsync(inventory);
        return inventory;
    }
}

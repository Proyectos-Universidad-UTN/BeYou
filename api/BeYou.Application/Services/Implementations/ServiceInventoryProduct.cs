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

public class ServiceInventoryProduct(ICoreService<InventoryProduct> coreService, IMapper mapper,
                                    IValidator<InventoryProduct> inventoryProductValidator) : IServiceInventoryProduct
{
    /// <inheritdoc />
    public async Task<ResponseInventoryProductDto> CreateInventoryProductAsync(RequestInventoryProductDto inventoryProductDto)
    {
        var inventoryProduct = await ValidateInventoryProductAsync(inventoryProductDto);

        var result = await coreService.UnitOfWork.Repository<InventoryProduct>().AddAsync(inventoryProduct);
        await coreService.UnitOfWork.SaveChangesAsync();

        if (result == null) throw new NotFoundException("Inventario producto no creado.");

        return mapper.Map<ResponseInventoryProductDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> CreateInventoryProductAsync(IEnumerable<RequestInventoryProductDto> inventoryProductsDto)
    {
        var inventoryProducts = await ValidateInventoryProductAsync(inventoryProductsDto);

        var result = await coreService.UnitOfWork.Repository<InventoryProduct>().AddRangeAsync(inventoryProducts.ToList());
        await coreService.UnitOfWork.SaveChangesAsync();

        if (result == null) throw new ListNotAddedException("Error al guardar inventario productos.");

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseInventoryProductDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<InventoryProduct>(x => x.Id == id);
        var inventoryProduct = await coreService.UnitOfWork.Repository<InventoryProduct>().FirstOrDefaultAsync(spec);
        if (inventoryProduct == null) throw new NotFoundException("Inventario producto no encontrado.");

        return mapper.Map<ResponseInventoryProductDto>(inventoryProduct);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInventoryProductDto>> ListAllByInventoryAsync(long inventoryId)
    {
        var spec = new BaseSpecification<InventoryProduct>(x => x.InventoryId == inventoryId);
        var inventoryProducts = await coreService.UnitOfWork.Repository<InventoryProduct>().ListAsync(spec);

        return mapper.Map<ICollection<ResponseInventoryProductDto>>(inventoryProducts);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInventoryProductDto>> ListAllByProductAsync(long productId)
    {
        var spec = new BaseSpecification<InventoryProduct>(x => x.ProductId == productId);
        var inventoryProducts = await coreService.UnitOfWork.Repository<InventoryProduct>().ListAsync(spec);

        return mapper.Map<ICollection<ResponseInventoryProductDto>>(inventoryProducts);
    }

    /// <inheritdoc />
    public async Task<ResponseInventoryProductDto> UpdateInventoryProductAsync(long inventoryProductId, RequestInventoryProductDto inventoryProductDto)
    {
        if (!await coreService.UnitOfWork.Repository<InventoryProduct>().ExistsAsync(inventoryProductId)) throw new NotFoundException("Inventario producto no encontrada.");

        var inventoryProduct = await ValidateInventoryProductAsync(inventoryProductDto);
        inventoryProduct.Id = inventoryProductId;

        coreService.UnitOfWork.Repository<InventoryProduct>().Update(inventoryProduct);
        await coreService.UnitOfWork.SaveChangesAsync();

        return await FindByIdAsync(inventoryProductId);
    }

    /// <summary>
    /// Validate inventory product request model
    /// </summary>
    /// <param name="inventoryProductDto">Inventory product request model to be added</param>
    /// <returns>InventoryProduct</returns>
    private async Task<InventoryProduct> ValidateInventoryProductAsync(RequestInventoryProductDto inventoryProductDto)
    {
        var inventoryProduct = mapper.Map<InventoryProduct>(inventoryProductDto);
        await inventoryProductValidator.ValidateAndThrowAsync(inventoryProduct);
        return inventoryProduct;
    }

    /// <summary>
    /// Validate inventory products request model
    /// </summary>
    /// <param name="inventoryProductsDto">Inventory products request model to be added</param>
    /// <returns>IEnumerable of InventoryProduct</returns>
    private async Task<IEnumerable<InventoryProduct>> ValidateInventoryProductAsync(IEnumerable<RequestInventoryProductDto> inventoryProductsDto)
    {
        var inventoryProducts = mapper.Map<List<InventoryProduct>>(inventoryProductsDto);
        foreach (var item in inventoryProducts)
        {
            await inventoryProductValidator.ValidateAndThrowAsync(item);
        }
        return inventoryProducts;
    }
}

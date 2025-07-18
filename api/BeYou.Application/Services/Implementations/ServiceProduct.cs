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

public class ServiceProduct(ICoreService<Product> coreService, IMapper mapper,
                                  IValidator<Product> productValidator) : IServiceProduct
{

    private readonly string[] ProductWithCategory = ["Category"];

    /// <inheritdoc />
    public async Task<ResponseProductDto> CreateProductAsync(RequestProductDto productDTO)
    {
        var product = await ValidateProductAsync(productDTO);

        var result = await coreService.UnitOfWork.Repository<Product>().AddAsync(product);
        await coreService.UnitOfWork.SaveChangesAsync();
        if (result == null) throw new NotFoundException("Producto no creado.");

        return mapper.Map<ResponseProductDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseProductDto> UpdateProductAsync(long id, RequestProductDto productDTO)
    {
        var spec = new BaseSpecification<Product>(x => x.Id == id);
        var existingProduct = await coreService.UnitOfWork.Repository<Product>().FirstOrDefaultAsync(spec);

        if (existingProduct == null)
            throw new NotFoundException("Producto no encontrado.");

        // Validación (si es necesaria, podés validar aquí)
        await ValidateProductAsync(productDTO);

        // Mapear los nuevos datos sobre la entidad existente
        mapper.Map(productDTO, existingProduct);

        // Preservar campos críticos
        existingProduct.Active = existingProduct.Active;
        existingProduct.Created = existingProduct.Created;
        existingProduct.CreatedBy = existingProduct.CreatedBy;

        coreService.UnitOfWork.Repository<Product>().Update(existingProduct);
        await coreService.UnitOfWork.SaveChangesAsync();

        return await FindByIdAsync(id);
    }


    /// <inheritdoc />
    public async Task<ResponseProductDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<Product>(x => x.Id == id);
        var product = await coreService.UnitOfWork.Repository<Product>().FirstOrDefaultAsync(spec);
        if (product == null) throw new NotFoundException("Producto no encontrado.");

        return mapper.Map<ResponseProductDto>(product);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseProductDto>> ListAllAsync(bool excludeProductsInventory = false, long inventoryId = 0)
    {
        if (!excludeProductsInventory)
        {
            var products = await coreService.UnitOfWork.Repository<Product>().ListAllAsync();
            return mapper.Map<ICollection<ResponseProductDto>>(products);
        }

        var queryExcluded = from a in coreService.UnitOfWork.Repository<Product>().AsQueryable()
                            join b in coreService.UnitOfWork.Repository<InventoryProduct>().AsQueryable() on a.Id equals b.ProductId
                            join c in coreService.UnitOfWork.Repository<Inventory>().AsQueryable() on b.InventoryId equals c.Id
                            where c.Id == inventoryId
                            select a;

        var query = coreService.UnitOfWork.Repository<Product>().AsQueryable().Except(queryExcluded);

        var productsFiltered = await coreService.UnitOfWork.Repository<Product>().ListAsync(query, ProductWithCategory);

        return mapper.Map<ICollection<ResponseProductDto>>(productsFiltered);
    }

    /// <summary>
    /// Validate product
    /// </summary>
    /// <param name="productDTO">Product request model to be added/updated</param>
    /// <returns>Product</returns>
    private async Task<Product> ValidateProductAsync(RequestProductDto productDTO)
    {
        var product = mapper.Map<Product>(productDTO);
        await productValidator.ValidateAndThrowAsync(product);
        return product;
    }
}

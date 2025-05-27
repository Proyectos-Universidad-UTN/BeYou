using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceInventoryProduct
{
    /// <summary>
    /// Get inventory product with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseInventoryProductDto</returns>
    Task<ResponseInventoryProductDto> FindByIdAsync(long id);

    /// <summary>
    /// Get list of all inventory product by inventory
    /// </summary>
    /// <param name="inventoryId">Inventory id</param>
    /// <returns>ICollection of ResponseInventoryProductDto</returns>
    Task<ICollection<ResponseInventoryProductDto>> ListAllByInventoryAsync(long inventoryId);

    /// <summary>
    /// Get list of all inventory product by product
    /// </summary>
    /// <param name="productId">Product id</param>
    /// <returns>ICollection of ResponseInventoryProductDto</returns>
    Task<ICollection<ResponseInventoryProductDto>> ListAllByProductAsync(long productId);

    /// <summary>
    /// Create inventory product
    /// </summary>
    /// <param name="inventoryProductDto">Inventary product request model to be added</param>
    /// <returns>ResponseInventoryProductDto</returns>
    Task<ResponseInventoryProductDto> CreateInventoryProductAsync(RequestInventoryProductDto inventoryProductDto);

    /// <summary>
    /// Create inventory products
    /// </summary>
    /// <param name="inventoryProductsDto">List of Inventary product request model to be added</param>
    /// <returns>bool</returns>
    Task<bool> CreateInventoryProductAsync(IEnumerable<RequestInventoryProductDto> inventoryProductsDto);

    /// <summary>
    /// Update inventory product
    /// </summary>
    /// <param name="inventoryProductId">Inventory Product id</param>
    /// <param name="inventoryProductDto">Inventary product request model to be updated</param>
    /// <returns>ResponseInventoryProductDto</returns>
    Task<ResponseInventoryProductDto> UpdateInventoryProductAsync(long inventoryProductId, RequestInventoryProductDto inventoryProductDto);
}

using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;


namespace BeYou.Application.Services.Interfaces;

public interface IServiceProduct
{
    /// <summary>
    /// Get list of all products
    /// </summary>
    /// <param name="excludeProductsInventory">Indicator to excludes products from specific inventory</param>
    /// <param name="inventoryId">Inventory id to be excluded</param>
    /// <returns>ICollection of ResponseProductDto</returns>
    Task<ICollection<ResponseProductDto>> ListAllAsync(bool excludeProductsInventory = false, long inventoryId = 0);

    /// <summary>
    /// Get producto with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseProductDto</returns>
    Task<ResponseProductDto> FindByIdAsync(long id);

    /// <summary>
    /// Create product
    /// </summary>
    /// <param name="productDTO">Product request model to be added</param>
    /// <returns>ResponseProductDto</returns>
    Task<ResponseProductDto> CreateProductAsync(RequestProductDto productDTO);

    /// <summary>
    /// Update product
    /// </summary>
    /// <param name="id">Product if</param>
    /// <param name="productDTO">Product request model to be updated</param>
    /// <returns>ResponseProductDto</returns>
    Task<ResponseProductDto> UpdateProductAsync(long id, RequestProductDto productDTO);
}

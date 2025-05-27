using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceInventoryProductTransaction
{
    /// <summary>
    /// Get list of all inventory product movements by inventory
    /// </summary>
    /// <param name="inventoryId">Inventory id</param>
    /// <returns>ICollection of ResponseInventoryProductTransactionDto</returns>
    Task<ICollection<ResponseInventoryProductTransactionDto>> ListAllByInventoryAsync(long inventoryId);

    /// <summary>
    /// Get list of all inventory product movements by product
    /// </summary>
    /// <param name="productId">Product id</param>
    /// <returns>ICollection of ResponseInventoryProductTransactionDto</returns>
    Task<ICollection<ResponseInventoryProductTransactionDto>> ListAllByProductAsync(long productId);

    /// <summary>
    /// Create inventory product movement
    /// </summary>
    /// <param name="inventoryProductTransactionDto">Inventory product movement to be added</param>
    /// <returns>bool</returns>
    Task<bool> CreateInventoryProductTransactionAsync(RequestInventoryProductTransactionDto inventoryProductTransactionDto);
}

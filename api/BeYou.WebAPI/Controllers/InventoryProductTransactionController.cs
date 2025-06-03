using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.WebAPI.Configuration;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller for managing inventory product movements.
/// </summary>
/// <param name="serviceInventoryProductTransaction">The service used for inventory product movement operations.</param>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class InventoryProductTransactionController(IServiceInventoryProductTransaction serviceInventoryProductTransaction) : ControllerBase
{
    /// <summary>
    /// Creates a new inventory product movement record.
    /// </summary>
    /// <param name="inventoryProductTransaction">The inventory product movement data to be created.</param>
    /// <returns>The details of the created inventory product movement.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseInventoryProductTransactionDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateInventoryProductTransactionAsync([FromBody] RequestInventoryProductTransactionDto inventoryProductTransaction)
    {
        ArgumentNullException.ThrowIfNull(inventoryProductTransaction);
        var result = await serviceInventoryProductTransaction.CreateInventoryProductTransactionAsync(inventoryProductTransaction);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Retrieves all inventory product movements for a given inventory.
    /// </summary>
    /// <param name="inventoryId">The ID of the inventory.</param>
    /// <returns>A list of inventory product movements for the specified inventory.</returns>
    [HttpGet("~/api/Inventory/{inventoryId}/Transactions")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseInventoryProductTransactionDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllByInventoryAsync(short inventoryId)
    {
        var inventories = await serviceInventoryProductTransaction.ListAllByInventoryAsync(inventoryId);
        return StatusCode(StatusCodes.Status200OK, inventories);
    }

    /// <summary>
    /// Retrieves all inventory product movements for a given product.
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    /// <returns>A list of inventory product movements for the specified product.</returns>
    [HttpGet("~/api/Product/{productId}/Transactions")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseInventoryProductTransactionDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllByProductAsync(short productId)
    {
        var inventories = await serviceInventoryProductTransaction.ListAllByProductAsync(productId);
        return StatusCode(StatusCodes.Status200OK, inventories);
    }
}
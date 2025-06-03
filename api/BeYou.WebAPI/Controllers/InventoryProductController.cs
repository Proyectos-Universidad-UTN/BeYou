using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.WebAPI.Configuration;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller for managing inventory products.
/// </summary>
/// <param name="serviceInventoryProduct">The service used for inventory product operations.</param>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class InventoryProductController(IServiceInventoryProduct serviceInventoryProduct) : ControllerBase
{
    /// <summary>
    /// Retrieves a specific inventory product by its ID.
    /// </summary>
    /// <param name="inventoryProductId">The ID of the inventory product.</param>
    /// <returns>The details of the specified inventory product.</returns>
    [HttpGet("{inventoryProductId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseInventoryProductDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(long inventoryProductId)
    {
        var inventoryProduct = await serviceInventoryProduct.FindByIdAsync(inventoryProductId);
        return StatusCode(StatusCodes.Status200OK, inventoryProduct);
    }

    /// <summary>
    /// Retrieves all inventory products for a given inventory.
    /// </summary>
    /// <param name="inventoryId">The ID of the inventory.</param>
    /// <returns>A list of inventory products for the specified inventory.</returns>
    [HttpGet("~/api/Inventory/{inventoryId}/Products")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseInventoryProductDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllByInventoryAsync(short inventoryId)
    {
        var inventoryProducts = await serviceInventoryProduct.ListAllByInventoryAsync(inventoryId);
        return StatusCode(StatusCodes.Status200OK, inventoryProducts);
    }

    /// <summary>
    /// Retrieves all inventory products for a given product.
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    /// <returns>A list of inventory products for the specified product.</returns>
    [HttpGet("~/api/Product/{productId}/Inventories")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseInventoryProductDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllByProductAsync(short productId)
    {
        var inventoryProducts = await serviceInventoryProduct.ListAllByProductAsync(productId);
        return StatusCode(StatusCodes.Status200OK, inventoryProducts);
    }

    /// <summary>
    /// Creates a new inventory product.
    /// </summary>
    /// <param name="inventoryProduct">The inventory product data to be created.</param>
    /// <returns>The details of the created inventory product.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseInventoryProductDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateInventoryProductAsync([FromBody] RequestInventoryProductDto inventoryProduct)
    {
        ArgumentNullException.ThrowIfNull(inventoryProduct);
        var result = await serviceInventoryProduct.CreateInventoryProductAsync(inventoryProduct);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Creates multiple inventory products in bulk.
    /// </summary>
    /// <param name="inventoryProduct">A collection of inventory product data to be created.</param>
    /// <returns>A boolean indicating whether the creation was successful.</returns>
    [HttpPost("Bulk")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateInventoryProductAsync([FromBody] IEnumerable<RequestInventoryProductDto> inventoryProduct)
    {
        ArgumentNullException.ThrowIfNull(inventoryProduct);
        var result = await serviceInventoryProduct.CreateInventoryProductAsync(inventoryProduct);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Updates an existing inventory product.
    /// </summary>
    /// <param name="inventoryProductId">The ID of the inventory product to update.</param>
    /// <param name="inventoryProduct">The updated inventory product data.</param>
    /// <returns>The details of the updated inventory product.</returns>
    [HttpPut("{inventoryProductId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseInventoryProductDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> UpdateInventoryProductAsync(long inventoryProductId, [FromBody] RequestInventoryProductDto inventoryProduct)
    {
        ArgumentNullException.ThrowIfNull(inventoryProduct);
        var result = await serviceInventoryProduct.UpdateInventoryProductAsync(inventoryProductId, inventoryProduct);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}
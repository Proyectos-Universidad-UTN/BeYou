using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller for managing products (Products).
/// </summary>
/// <param name="serviceProduct">The service used for product operations.</param>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class ProductController(IServiceProduct serviceProduct) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of products, optionally excluding those associated with a specified inventory.
    /// </summary>
    /// <param name="excludeProductsInventory">Whether to exclude products associated with the inventory.</param>
    /// <param name="inventoryId">The ID of the inventory to filter products by.</param>
    /// <returns>A list of products.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseProductDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync([FromQuery] bool excludeProductsInventory = false, [FromQuery] short inventoryId = 0)
    {
        var products = await serviceProduct.ListAllAsync(excludeProductsInventory, inventoryId);
        return StatusCode(StatusCodes.Status200OK, products);
    }

    /// <summary>
    /// Retrieves a product by its ID.
    /// </summary>
    /// <param name="productId">The ID of the product to retrieve.</param>
    /// <returns>The details of the specified product.</returns>
    [HttpGet("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseProductDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(short productId)
    {
        var product = await serviceProduct.FindByIdAsync(productId);
        return StatusCode(StatusCodes.Status200OK, product);
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="product">The product data to be created.</param>
    /// <returns>The details of the created product.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseProductDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateProductAsync([FromBody] RequestProductDto product)
    {
        ArgumentNullException.ThrowIfNull(product);
        var result = await serviceProduct.CreateProductAsync(product);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Updates an existing product by its ID.
    /// </summary>
    /// <param name="productId">The ID of the product to update.</param>
    /// <param name="product">The updated product data.</param>
    /// <returns>The updated details of the product.</returns>
    [HttpPut("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseProductDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> UpdateProductAsync(short productId, [FromBody] RequestProductDto product)
    {
        ArgumentNullException.ThrowIfNull(product);
        var result = await serviceProduct.UpdateProductAsync(productId, product);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}
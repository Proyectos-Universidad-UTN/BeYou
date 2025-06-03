using Asp.Versioning;
using BeYou.Application.Configuration.Pagination;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.WebAPI.Configuration;
using BeYou.Application.Services.Interfaces;
using BeYou.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller for managing suppliers (Vendors).
/// </summary>
/// <param name="serviceVendor">The service used for supplier operations.</param>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class VendorController(IServiceVendor serviceVendor) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of suppliers with optional pagination.
    /// </summary>
    /// <param name="paginationParameters">Optional pagination parameters.</param>
    /// <returns>A list of suppliers, potentially paginated.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseVendorDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync([FromQuery] PaginationParameters? paginationParameters = null)
    {
        if (!paginationParameters!.Paginated) return StatusCode(StatusCodes.Status200OK, await serviceVendor.ListAllAsync());

        var paginated = await serviceVendor.ListAllAsync(paginationParameters);

        var metadata = new
        {
            paginated.TotalCount,
            paginated.PageSize,
            paginated.CurrentPage,
            paginated.TotalPages,
            paginated.HasNext,
            paginated.HasPrevious
        };

        Response.Headers.Append("X-Pagination", Serialization.Serialize(metadata));

        return StatusCode(StatusCodes.Status200OK, paginated);
    }

    /// <summary>
    /// Retrieves a supplier by its ID.
    /// </summary>
    /// <param name="vendorId">The ID of the supplier to retrieve.</param>
    /// <returns>The details of the specified supplier.</returns>
    [HttpGet("{vendorId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseVendorDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(byte vendorId)
    {
        var result = await serviceVendor.FindByIdAsync(vendorId);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Creates a new supplier.
    /// </summary>
    /// <param name="vendor">The supplier data to be created.</param>
    /// <returns>The details of the created supplier.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseVendorDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateVendorAsync([FromBody] RequestVendorDto vendor)
    {
        ArgumentNullException.ThrowIfNull(vendor);
        var result = await serviceVendor.CreateVendorAsync(vendor);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Updates an existing supplier by its ID.
    /// </summary>
    /// <param name="vendorId">The ID of the supplier to update.</param>
    /// <param name="vendor">The updated supplier data.</param>
    /// <returns>The updated details of the supplier.</returns>
    [HttpPut("{vendorId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseVendorDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> UpdateVendorAsync(byte vendorId, [FromBody] RequestVendorDto vendor)
    {
        ArgumentNullException.ThrowIfNull(vendor);
        var result = await serviceVendor.UpdateVendorAsync(vendorId, vendor);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Deletes a supplier by its ID.
    /// </summary>
    /// <param name="vendorId">The ID of the supplier to delete.</param>
    /// <returns>A boolean indicating the success of the deletion.</returns>
    [HttpDelete("{vendorId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> DeleteVendorAsync(byte vendorId)
    {
        var result = await serviceVendor.DeleteVendorAsync(vendorId);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}
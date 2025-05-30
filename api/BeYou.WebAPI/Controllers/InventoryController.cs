using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller for managing inventory operations.
/// </summary>
/// <param name="serviceInventory">The service used for inventory operations.</param>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class InventoryController(IServiceInventory serviceInventory) : ControllerBase
{
    /// <summary>
    /// Retrieves all inventories for a given branch.
    /// </summary>
    /// <param name="inventoryId">The ID of the branch.</param>
    /// <returns>A list of inventories for the specified branch.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseInventoryDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllByBranchAsync(byte inventoryId)
    {
        var inventories = await serviceInventory.ListAllByBranchAsync(inventoryId);
        return StatusCode(StatusCodes.Status200OK, inventories);
    }

    /// <summary>
    /// Retrieves a specific inventory by its ID.
    /// </summary>
    /// <param name="inventoryId">The ID of the inventory.</param>
    /// <returns>The details of the specified inventory.</returns>
    [HttpGet("{inventoryId}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseInventoryDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(short inventoryId)
    {
        var inventory = await serviceInventory.FindByIdAsync(inventoryId);
        return StatusCode(StatusCodes.Status200OK, inventory);
    }

    /// <summary>
    /// Creates a new inventory for a given branch.
    /// </summary>
    /// <param name="branchId">The ID of the branch.</param>
    /// <param name="inventory">The inventory data to be created.</param>
    /// <returns>The details of the created inventory.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseInventoryDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateInventoryAsync(byte branchId, [FromBody] RequestInventoryDto inventory)
    {
        ArgumentNullException.ThrowIfNull(inventory);
        var result = await serviceInventory.CreateInventoryAsync(branchId, inventory);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Updates an existing inventory for a given branch.
    /// </summary>
    /// <param name="branchId">The ID of the branch.</param>
    /// <param name="inventoryId">The ID of the inventory to update.</param>
    /// <param name="inventory">The updated inventory data.</param>
    /// <returns>The details of the updated inventory.</returns>
    [HttpPut("{inventoryId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseInventoryDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> UpdateInventoryAsync(byte branchId, short inventoryId, [FromBody] RequestInventoryDto inventory)
    {
        ArgumentNullException.ThrowIfNull(inventory);
        var result = await serviceInventory.UpdateInventoryAsync(branchId, inventoryId, inventory);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Deletes a specific inventory by its ID.
    /// </summary>
    /// <param name="inventoryId">The ID of the inventory to delete.</param>
    /// <returns>A boolean indicating whether the deletion was successful.</returns>
    [HttpDelete("~/api/[controller]/{inventoryId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> DeleteFeriado(short inventoryId)
    {
        var inventory = await serviceInventory.DeleteInventoryAsync(inventoryId);
        return StatusCode(StatusCodes.Status200OK, inventory);
    }
}
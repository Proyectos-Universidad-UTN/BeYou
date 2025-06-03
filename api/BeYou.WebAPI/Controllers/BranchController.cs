using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Enums;
using BeYou.Application.Services.Interfaces;
using BeYou.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller in charge of branch calls
/// </summary>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class BranchController(IServiceBranch serviceBranch) : ControllerBase
{
    /// <summary>
    /// Get list of all branches
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseBranchDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var branches = await serviceBranch.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, branches);
    }

    /// <summary>
    /// Get list of all branches by role from user logged in 
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet("ByRol")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseBranchDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllByRoleAsync()
    {
        var branch = await serviceBranch.ListAllByRoleAsync();
        return StatusCode(StatusCodes.Status200OK, branch);
    }

    /// <summary>
    /// Get branch with specific id
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{branchId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBranchDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(byte branchId)
    {
        var branch = await serviceBranch.FindByIdAsync(branchId);
        return StatusCode(StatusCodes.Status200OK, branch);
    }

    /// <summary>
    /// Create new branch
    /// </summary>
    /// <param name="branch">Branch request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [BeYouAuthorize(RoleApplication.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseBranchDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateBranchAsync([FromBody] RequestBranchDto branch)
    {
        ArgumentNullException.ThrowIfNull(branch);
        var result = await serviceBranch.CreateBranchAsync(branch);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Update existing branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="branch">Branch request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{branchId}")]
    [BeYouAuthorize(RoleApplication.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBranchDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> UpdateBranchAsync(byte branchId, [FromBody] RequestBranchDto branch)
    {
        ArgumentNullException.ThrowIfNull(branch);
        var result = await serviceBranch.UpdateBranchAsync(branchId, branch);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Deletes a branch by its ID.
    /// </summary>
    /// <param name="branchId">The ID of the branch to delete.</param>
    /// <returns>The deleted branch.</returns>
    [HttpDelete("{branchId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> DeleteBranchAsync(byte branchId)
    {
        var branch = await serviceBranch.DeleteBranchAsync(branchId);
        return StatusCode(StatusCodes.Status200OK, branch);
    }
}
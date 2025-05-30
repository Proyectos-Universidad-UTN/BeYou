using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller in charge of user's branch calls
/// </summary>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class UserBranchController(IServiceUserBranch serviceUserBranch) : ControllerBase
{
    /// <summary>
    /// Assign users to specific branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="usersBranchDto">List of users</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/Branch/{branchId}/Users")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateUserBranchAsync(byte branchId, [FromBody] IEnumerable<RequestUserBranchDto> usersBranchDto)
    {
        ArgumentNullException.ThrowIfNull(usersBranchDto);
        var result = await serviceUserBranch.CreateUserBranchAsync(branchId, usersBranchDto);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Check if user is available to assign to a branch
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="branchId">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Branch/{id}/Branch/{branchId}/availability")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> IsAvailableAsync(short id, byte branchId)
    {
        var available = await serviceUserBranch.IsAvailableAsync(id, branchId);
        return StatusCode(StatusCodes.Status200OK, available);
    }
}
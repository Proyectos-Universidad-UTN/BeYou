using Asp.Versioning;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller in charge of role calls
/// </summary>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class RoleController(IServiceRole serviceRole) : ControllerBase
{
    /// <summary>
    /// Get list of all roles
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseRoleDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var roles = await serviceRole.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, roles);
    }
}
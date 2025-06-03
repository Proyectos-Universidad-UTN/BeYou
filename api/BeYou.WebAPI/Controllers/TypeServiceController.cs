using Asp.Versioning;
using BeYou.Application.Dtos.Response;
using BeYou.WebAPI.Configuration;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller in charge of service type calls
/// </summary>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class TypeServiceController(IServiceTypeService serviceTypeService) : ControllerBase
{
    /// <summary>
    /// Get list of all service types
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseTypeServiceDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var serviceTypes = await serviceTypeService.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, serviceTypes);
    }
}
using Asp.Versioning;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the CantonController with the specified service canton.
/// </summary>
/// <param name="serviceCanton">The service used for canton operations.</param>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class CantonController(IServiceCanton serviceCanton) : ControllerBase
{
    /// <summary>
    /// Retrieves all cantons associated with a specific province.
    /// </summary>
    /// <param name="provinceId">The ID of the province.</param>
    /// <returns>A list of cantons within the specified province.</returns>
    [HttpGet("~/api/Province/{provinceId}/Canton")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseCantonDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllByByProvinceAsync(byte provinceId)
    {
        var cantons = await serviceCanton.ListAllByProvinceAsync(provinceId);
        return StatusCode(StatusCodes.Status200OK, cantons);
    }

    /// <summary>
    /// Retrieves details of a specific canton by its ID.
    /// </summary>
    /// <param name="id">The ID of the canton.</param>
    /// <returns>The details of the specified canton.</returns>
    [HttpGet("{idCanton}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseCantonDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(byte id)
    {
        var cantons = await serviceCanton.FindByIdAsync(id);
        return StatusCode(StatusCodes.Status200OK, cantons);
    }
}
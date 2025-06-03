using Asp.Versioning;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller in charge of province calls
/// </summary>
[ApiController]
[AllowAnonymous]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class ProvinceController(IServiceProvince serviceProvince) : ControllerBase
{
    /// <summary>
    /// Get list of all provinces
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseProvinceDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var provinces = await serviceProvince.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, provinces);
    }

    /// <summary>
    /// Get province with specific id
    /// </summary>
    /// <param name="provinceId">Province id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{provinceId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseProvinceDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(byte provinceId)
    {
        var province = await serviceProvince.FindByIdAsync(provinceId);
        return StatusCode(StatusCodes.Status200OK, province);
    }
}
using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Enums;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller in charge of service calls
/// </summary>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class ServiceController(IServiceService serviceService) : ControllerBase
{
    /// <summary>
    /// Get list of all services
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseServiceDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var services = await serviceService.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, services);
    }

    /// <summary>
    /// Get service with specific id
    /// </summary>
    /// <param name="serviceId">Service id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{serviceId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseServiceDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(byte serviceId)
    {
        var service = await serviceService.FindByIdAsync(serviceId);
        return StatusCode(StatusCodes.Status200OK, service);
    }

    /// <summary>
    /// Create new service
    /// </summary>
    /// <param name="service">Service request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [BaseReservationAuthorize(RoleApplication.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseServiceDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateServiceAsync([FromBody] RequestServiceDto service)
    {
        ArgumentNullException.ThrowIfNull(service);
        var result = await serviceService.CreateServiceAsync(service);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Update existing service
    /// </summary>
    /// <param name="serviceId">Service id</param>
    /// <param name="service">Service request model to be updated</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{serviceId}")]
    [BaseReservationAuthorize(RoleApplication.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseServiceDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> UpdateServiceAsync(byte serviceId, [FromBody] RequestServiceDto service)
    {
        ArgumentNullException.ThrowIfNull(service);
        var result = await serviceService.UpdateServiceAsync(serviceId, service);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Deletes a service by its ID.
    /// </summary>
    /// <param name="serviceId">The ID of the service to delete.</param>
    /// <returns>The deleted service.</returns>
    [HttpDelete("{serviceId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseServiceDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> DeleteServiceAsync(byte serviceId)
    {
        var service = await serviceService.DeleteServiceAsync(serviceId);
        return StatusCode(StatusCodes.Status200OK, service);
    }
}
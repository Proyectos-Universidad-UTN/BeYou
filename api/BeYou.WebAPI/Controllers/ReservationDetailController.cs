using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.WebAPI.Configuration;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller in charge of reservation detail calls
/// </summary>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class ReservationDetailController(IServiceReservationDetail serviceReservationDetail) : ControllerBase
{
    /// <summary>
    /// Get reservation detail with specific id
    /// </summary>
    /// <param name="reservationDetailId">Reservation detail Id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{reservationDetailId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseReservationDetailDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(int reservationDetailId)
    {
        var detailReservation = await serviceReservationDetail.FindByIdAsync(reservationDetailId);
        return StatusCode(StatusCodes.Status200OK, detailReservation);
    }

    /// <summary>
    /// Get list of all reservation's details by branch
    /// </summary>
    /// <param name="id">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Reservation/{id}/Detail")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseReservationDetailDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllByReservationAsync(int id)
    {
        var details = await serviceReservationDetail.ListAllByReservationAsync(id);
        return StatusCode(StatusCodes.Status200OK, details);
    }

    /// <summary>
    /// Create details reservation
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="detailsReservation">List of details to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/Reservation/{id}/Detail")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateReservationDetailAsync(int branchId, [FromBody] IEnumerable<RequestReservationDetailDto> detailsReservation)
    {
        ArgumentNullException.ThrowIfNull(detailsReservation);
        var result = await serviceReservationDetail.CreateReservationDetailAsync(branchId, detailsReservation);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}
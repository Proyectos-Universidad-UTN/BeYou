using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller in charge of reservations calls
/// </summary>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class ReservationController(IServiceReservation serviceReservation) : ControllerBase
{
    /// <summary>
    /// Get list of all reservations
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseReservationDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var reservations = await serviceReservation.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, reservations);
    }

    /// <summary>
    /// Get list of all reservations by branch with filter option with start date and end date
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Branch/{branchId}/reservations")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ICollection<ResponseReservationCalendarAgendaDto>>))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllByBranchAsync(byte branchId, [FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
    {
        var reservations = await serviceReservation.ListAllByBranchAsync(branchId, startDate, endDate);
        return StatusCode(StatusCodes.Status200OK, reservations);
    }

    /// <summary>
    /// Get list of all reservations by branch
    /// </summary>
    /// <param name="reservationId">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{reservationId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseReservationDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(int reservationId)
    {
        var reservation = await serviceReservation.FindByIdAsync(reservationId);
        return StatusCode(StatusCodes.Status200OK, reservation);
    }

    /// <summary>
    /// Get availablity for a branch in specific date
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="date">Date to filter</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Branch/{branchId}/date-availability/{date}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<TimeOnly>))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ScheduleAvailabilityBranchAsync(byte branchId, DateTime date)
    {
        var availablesHours = await serviceReservation.ScheduleAvailabilityBranchAsync(branchId, new DateOnly(date.Year, date.Month, date.Day));
        return StatusCode(StatusCodes.Status200OK, availablesHours);
    }

    /// <summary>
    /// Create a new reservation
    /// </summary>
    /// <param name="reservation">Reservation request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseReservationDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateReservationAsync([FromBody] RequestReservationDto reservation)
    {
        //retorna una excepçión is es nulo
        ArgumentNullException.ThrowIfNull(reservation);
        var result = await serviceReservation.CreateReservationAsync(reservation);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Update an existing reservation
    /// </summary>
    /// <param name="serviceId">Service id</param>
    /// <param name="reservation">Reservation request model to be updated</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{reservationId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseReservationDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> UpdateReservationAsync(int serviceId, [FromBody] RequestReservationDto reservation)
    {
        //retorna una excepçión is es nulo
        ArgumentNullException.ThrowIfNull(reservation);
        var result = await serviceReservation.UpdateReservationAsync(serviceId, reservation);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}
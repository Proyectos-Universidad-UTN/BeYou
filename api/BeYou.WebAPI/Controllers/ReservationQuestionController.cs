using Asp.Versioning;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller in charge of reservation question calls
/// </summary>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class ReservationQuestionController(IServiceReservationQuestion serviceReservationQuestion) : ControllerBase
{
    /// <summary>
    /// Get list of all reservation's questions
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseReservationQuestionDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var reservationQuestions = await serviceReservationQuestion.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, reservationQuestions);
    }

    /// <summary>
    /// Get reservation question with specific id
    /// </summary>
    /// <param name="reservationQuestionId">Reservation question Id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{reservationQuestionId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseReservationQuestionDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(int reservationQuestionId)
    {
        var reservationQuestion = await serviceReservationQuestion.FindByIdAsync(reservationQuestionId);
        return StatusCode(StatusCodes.Status200OK, reservationQuestion);
    }
}
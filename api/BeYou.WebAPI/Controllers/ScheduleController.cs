using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the controller with the specified schedule service.
/// </summary>
/// <param name="serviceSchedule">The service used for schedule operations.</param>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class ScheduleController(IServiceSchedule serviceSchedule) : ControllerBase
{
    /// <summary>
    /// Retrieves all schedules.
    /// </summary>
    /// <returns>A collection of all schedules.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseScheduleDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var schedules = await serviceSchedule.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, schedules);
    }

    /// <summary>
    /// Retrieves a specific schedule by its ID.
    /// </summary>
    /// <param name="scheduleId">The ID of the schedule.</param>
    /// <returns>The details of the specified schedule.</returns>
    [HttpGet("{scheduleId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseScheduleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(short scheduleId)
    {
        var schedule = await serviceSchedule.FindByIdAsync(scheduleId);
        return StatusCode(StatusCodes.Status200OK, schedule);
    }

    /// <summary>
    /// Creates a new schedule.
    /// </summary>
    /// <param name="schedule">The schedule data to be created.</param>
    /// <returns>The created schedule.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseScheduleDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateScheduleAsync([FromBody] RequestScheduleDto schedule)
    {
        ArgumentNullException.ThrowIfNull(schedule);
        var result = await serviceSchedule.CreateScheduleAsync(schedule);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Updates an existing schedule by its ID.
    /// </summary>
    /// <param name="scheduleId">The ID of the schedule to update.</param>
    /// <param name="schedule">The updated schedule data.</param>
    /// <returns>The updated schedule.</returns>
    [HttpPut("{scheduleId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseScheduleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> UpdateScheduleAsync(short scheduleId, [FromBody] RequestScheduleDto schedule)
    {
        ArgumentNullException.ThrowIfNull(schedule);
        var result = await serviceSchedule.UpdateScheduleAsync(scheduleId, schedule);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Deletes a specific schedule by its ID.
    /// </summary>
    /// <param name="scheduleId">The ID of the schedule to delete.</param>
    /// <returns>IActionResult</returns>
    [HttpDelete("~/api/[controller]/{scheduleId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> DeleteScheduleAsync(short scheduleId)
    {
        var result = await serviceSchedule.DeleteScheduleAsync(scheduleId);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}
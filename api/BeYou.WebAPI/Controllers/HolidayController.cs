using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the controller with the specified holiday service.
/// </summary>
/// <param name="serviceHoliday">The service used for holiday operations.</param>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class HolidayController(IServiceHoliday serviceHoliday) : ControllerBase
{
    /// <summary>
    /// Retrieves all holidays.
    /// </summary>
    /// <returns>A list of all holidays.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseHolidayDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var holidays = await serviceHoliday.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, holidays);
    }

    /// <summary>
    /// Retrieves a specific holiday by its ID.
    /// </summary>
    /// <param name="holidayId">The ID of the holiday.</param>
    /// <returns>The details of the specified holiday.</returns>
    [HttpGet("{holidayId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseHolidayDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(byte holidayId)
    {
        var holiday = await serviceHoliday.FindByIdAsync(holidayId);
        return StatusCode(StatusCodes.Status200OK, holiday);
    }

    /// <summary>
    /// Creates a new holiday.
    /// </summary>
    /// <param name="holiday">The holiday data to be created.</param>
    /// <returns>The created holiday.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseHolidayDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateHolidayAsync([FromBody] RequestHolidayDto holiday)
    {
        ArgumentNullException.ThrowIfNull(holiday);
        var result = await serviceHoliday.CreateHolidayAsync(holiday);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Updates an existing holiday by its ID.
    /// </summary>
    /// <param name="holidayId">The ID of the holiday to update.</param>
    /// <param name="holiday">The updated holiday data.</param>
    /// <returns>The updated holiday.</returns>
    [HttpPut("{holidayId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseHolidayDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> UpdateHolidayAsync(byte holidayId, [FromBody] RequestHolidayDto holiday)
    {
        ArgumentNullException.ThrowIfNull(holiday);
        var result = await serviceHoliday.UpdateHolidayAsync(holidayId, holiday);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Deletes a holiday by its ID.
    /// </summary>
    /// <param name="holidayId">The ID of the holiday to delete.</param>
    /// <returns>The deleted holiday.</returns>
    [HttpDelete("{holidayId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> DeleteHolidayAsync(byte holidayId)
    {
        var holiday = await serviceHoliday.DeleteHolidayAsync(holidayId);
        return StatusCode(StatusCodes.Status200OK, holiday);
    }
}
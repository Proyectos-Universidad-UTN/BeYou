using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller in charge of branch's holiday calls
/// </summary>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class BranchHolidayController(IServiceBranchHoliday serviceBranchHoliday) : ControllerBase
{
    /// <summary>
    /// Get list of all branch's holidays
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="year">Year</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Branch/{branchId}/Holiday")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseBranchHolidayDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllByBranchAsync(byte branchId, [FromQuery] short? year)
    {
        short? yearSeach = null;
        if (year != null) yearSeach = year == 0 ? (short)DateTime.Now.Year : year.Value;
        var branchHolidays = await serviceBranchHoliday.ListAllByBranchAsync(branchId, yearSeach);
        return StatusCode(StatusCodes.Status200OK, branchHolidays);
    }

    /// <summary>
    /// Get branch holiday
    /// </summary>
    /// <param name="branchHolidayId">Branch holiday Id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{branchHolidayId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBranchHolidayDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(short branchHolidayId)
    {
        var branchHolidays = await serviceBranchHoliday.FindByIdAsync(branchHolidayId);
        return StatusCode(StatusCodes.Status200OK, branchHolidays);
    }

    /// <summary>
    /// Assign holidays to a branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="branchHolidays">List of holidays</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/Branch/{branchId}/Holiday")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateBranchHolidayAsync(byte branchId, [FromBody] IEnumerable<RequestBranchHolidayDto> branchHolidays)
    {
        ArgumentNullException.ThrowIfNull(branchHolidays);
        var result = await serviceBranchHoliday.CreateBranchHolidaysAsync(branchId, branchHolidays);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}
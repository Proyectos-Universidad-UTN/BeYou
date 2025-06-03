using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using BeYou.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller in charge of branch's schedule calls
/// </summary>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class BranchScheduleController(IServiceBranchSchedule serviceBranchSchedule) : ControllerBase
{
    /// <summary>
    /// Get branch schedule with specific id
    /// </summary>
    /// <param name="branchScheduleId">Branch schedule id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{branchScheduleId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBranchScheduleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(short branchScheduleId)
    {
        var schedule = await serviceBranchSchedule.FindByIdAsync(branchScheduleId);
        return StatusCode(StatusCodes.Status200OK, schedule);
    }

    /// <summary>
    /// Get schedules by branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Branch/{branchId}/Schedule")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBranchScheduleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllByBranchAsync(byte branchId)
    {
        var schedules = await serviceBranchSchedule.ListAllByBranchAsync(branchId);
        return StatusCode(StatusCodes.Status200OK, schedules);
    }

    /// <summary>
    /// Assign schedules to a branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="branchSchedule">List of schedules</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/Branch/{branchId}/Schedule")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateBranchScheduleAsync(byte branchId, [FromBody] IEnumerable<RequestBranchScheduleDto> branchSchedule)
    {
        ArgumentNullException.ThrowIfNull(branchSchedule);
        var result = await serviceBranchSchedule.CreateBranchScheduleAsync(branchId, branchSchedule);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}
using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller in charge of unit of measure calls
/// </summary>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class UnitMeasureController(IServiceUnitMeasure serviceUnitMeasure) : ControllerBase
{
    /// <summary>
    /// Get list of all unit of measures
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseUnitMeasureDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var unitsMeasure = await serviceUnitMeasure.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, unitsMeasure);
    }

    /// <summary>
    /// Get unit of measure by id 
    /// </summary>
    /// <param name="unitMeasureId">Unit of measure id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{unitMeasureId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseUnitMeasureDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> GetByIdAsync(long unitMeasureId)
    {
        var unitMeasure = await serviceUnitMeasure.FindByIdAsync(unitMeasureId);
        return StatusCode(StatusCodes.Status200OK, unitMeasure);
    }

    /// <summary>
    /// Create a new unit of measure 
    /// </summary>
    /// <param name="unitMeasureDto">Unit of measure data transfer object</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseUnitMeasureDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateAsync([FromBody] RequestUnitMeasureDto unitMeasureDto)
    {
        ArgumentNullException.ThrowIfNull(unitMeasureDto);
        var unitMeasure = await serviceUnitMeasure.CreateUnitMeasureAsync(unitMeasureDto);
        return StatusCode(StatusCodes.Status201Created, unitMeasure);
    }

    /// <summary>
    /// Update an existing unit of measure
    /// </summary>
    /// <param name="unitMeasureId">
    /// Unit of measure id
    /// </param>
    /// <param name="unitMeasureDto">
    /// Unit of measure data transfer object
    /// </param>
    /// <returns>IActionResult</returns> 
    [HttpPut("{unitMeasureId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseUnitMeasureDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> UpdateAsync(long unitMeasureId, [FromBody] RequestUnitMeasureDto unitMeasureDto)
    {
        ArgumentNullException.ThrowIfNull(unitMeasureDto);
        var unitMeasure = await serviceUnitMeasure.UpdateUnitMeasureAsync(unitMeasureId, unitMeasureDto);
        return StatusCode(StatusCodes.Status200OK, unitMeasure);
    }

    /// <summary>
    /// Delete an existing unit of measure
    /// </summary>
    /// <param name="unitMeasureId">
    /// Unit of measure id
    /// </param>
    /// <returns>
    /// IActionResult
    /// </returns>
    [HttpDelete("{unitMeasureId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> DeleteAsync(long unitMeasureId)
    {
        var result = await serviceUnitMeasure.DeleteUnitMeasureAsync(unitMeasureId);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}
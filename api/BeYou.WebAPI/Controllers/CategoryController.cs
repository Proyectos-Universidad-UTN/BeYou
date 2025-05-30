using Asp.Versioning;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the CategoryController with the specified category service.
/// </summary>
/// <param name="serviceCategory">The service responsible for category operations.</param>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class CategoryController(IServiceCategory serviceCategory) : ControllerBase
{
    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <returns>A list of all categories.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseCategoryDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var categories = await serviceCategory.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, categories);
    }
}
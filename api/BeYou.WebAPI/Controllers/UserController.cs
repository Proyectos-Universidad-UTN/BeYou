// UserController.cs
using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Enums;
using BeYou.Application.Services.Interfaces;
using BeYou.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

[ApiController]
[BeYouAuthorize(RoleApplication.ADMINISTRADOR)]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class UserController(IServiceUser serviceUser) : ControllerBase
{

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user data to be created.</param>
    /// <returns>The details of the created user.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseUserDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateUserAsync([FromBody] RequestUserDto user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var result = await serviceUser.CreateUserAsync(user);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Updates an existing user by ID.
    /// </summary>
    /// <param name="userId">The ID of the user to update.</param>
    /// <param name="user">The updated user data.</param>
    /// <returns>The updated user details.</returns>
    [HttpPut("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseUserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> UpdateUserAsync(long userId, [FromBody] RequestUserDto user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var result = await serviceUser.UpdateUserAsync(userId, user);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Get list of all users
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseUserDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var users = await serviceUser.ListAllAsync();
        return Ok(users);
    }

    /// <summary>
    /// Get list of all users by role
    /// </summary>
    /// <param name="role">Role to look for</param>
    /// <returns>IActionResult</returns>
    [HttpGet("ByRol/{role}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseUserDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync(string role)
    {
        var users = await serviceUser.ListAllAsync(role);
        return Ok(users);
    }
}
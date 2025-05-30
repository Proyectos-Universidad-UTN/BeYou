using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response.Authentication;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the AuthenticationController with the specified service identity.
/// </summary>
/// <param name="serviceIdentity">The service identity used for authentication operations.</param>
[ApiController]
[AllowAnonymous]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class AuthenticationController(IServiceIdentity serviceIdentity) : ControllerBase
{
    /// <summary>
    /// Logs in a user using the provided login model.
    /// </summary>
    /// <param name="loginModel">The login credentials.</param>
    /// <returns>An action result with authentication details or an error.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> LoginAsync([FromBody] RequestUserLoginDto loginModel)
    {
        var login = await serviceIdentity.LoginAsync(loginModel);
        return StatusCode(StatusCodes.Status200OK, login);
    }

    /// <summary>
    /// Refreshes the authentication token using the provided token model.
    /// </summary>
    /// <param name="request">The token refresh request.</param>
    /// <returns>An action result with the new authentication token or an error.</returns>
    [Route("refreshToken")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] TokenModel request)
    {
        var refreshToken = await serviceIdentity.RefreshTokenAsync(request);
        return StatusCode(StatusCodes.Status200OK, refreshToken);
    }
}
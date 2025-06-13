using Asp.Versioning;
using BeYou.Application.Enums;
using BeYou.Application.Services.Interfaces;
using BeYou.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[BeYouAuthorize(RoleApplication.ADMINISTRADOR)]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class UserController(IServiceUser serviceUser) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListAllAsync()
    {
        var users = await serviceUser.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, users);
    }

    [HttpGet("~/api/[controller]/ByRol/{rol}")]
    public async Task<IActionResult> ListAllAsync(string role)
    {
        var users = await serviceUser.ListAllAsync(role);
        return StatusCode(StatusCodes.Status200OK, users);
    }

    [HttpGet("me")]
    public IActionResult GetMe([FromServices] IServiceUserContext userContext)
    {
        var user = userContext.GetCurrentUser();
        return Ok(user);
    }
}
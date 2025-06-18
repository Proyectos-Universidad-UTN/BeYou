using BeYou.Application.Dtos.Response;
using BeYou.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers
{
    /// <summary>
    /// Controller responsible for returning the authenticated user's information.
    /// </summary>
    [BeYouAuthorize]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "BeYou")]
    public class MeController : ControllerBase
    {
        private readonly IServiceUserContext _userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeController"/> class.
        /// </summary>
        /// <param name="userContext">Service for retrieving the current user context.</param>
        public MeController(IServiceUserContext userContext)
        {
            _userContext = userContext;
        }

        /// <summary>
        /// Retrieves information about the currently authenticated user.
        /// </summary>
        /// <returns>Returns the current user's details or Unauthorized if no user is found.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseMeDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUserAuthenticated()
        {
            var user = await _userContext.GetCurrentUserAsync();
  
            return Ok(user);
        }
    }
}
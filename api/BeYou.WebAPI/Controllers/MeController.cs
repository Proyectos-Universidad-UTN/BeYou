using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers
{
    /// <summary>
    /// Controller responsible for returning the authenticated user's information.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        public IActionResult GetUserAuthenticated()
        {
            var user = _userContext.GetCurrentUser();
            if (user == null)
                return Unauthorized();

            return Ok(user);
        }
    }
}
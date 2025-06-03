using BeYou.Application.Dtos.Response.Authentication;
using BeYou.Application.Enums;
using Microsoft.AspNetCore.Authorization;

namespace BeYou.WebAPI.Authorization;

/// <summary>
/// User identity handler class
/// </summary>
public class UserIdentityHandler : AuthorizationHandler<IdentifiedUser>
{
    /// <summary>
    /// Handle requirement for claims of user logged in
    /// </summary>
    /// <param name="context">Authorization context</param>
    /// <param name="requirement">identified User</param>
    /// <returns>Task</returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdentifiedUser requirement)
    {
        var httpContext = (HttpContext)context.Resource!;
        var claimFinder = new ClaimFinder(context.User.Claims);

        if (claimFinder.UserId != null && claimFinder.Role != null && claimFinder.Email != null)
        {
            httpContext.Items["CurrentUser"] = new CurrentUser
            {
                UserId = short.Parse(claimFinder.UserId!.Value),
                Email = claimFinder.Email!.Value,
                Role = (RoleApplication)Enum.Parse(typeof(RoleApplication), claimFinder.Role!.Value.ToUpper())
            };
        }

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
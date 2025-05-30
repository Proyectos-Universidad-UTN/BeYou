using BeYou.Application.Enums;
using BeYou.Utils;
using Microsoft.AspNetCore.Authorization;

namespace BeYou.WebAPI.Configuration;

/// <summary>
/// Authorization attribute for controllers
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class BaseReservationAuthorizeAttribute : AuthorizeAttribute
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <returns></returns>
    public BaseReservationAuthorizeAttribute() : base() { }

    /// <summary>
    /// Overload constructor to pass list of roles
    /// </summary>
    /// <param name="roles">List of roles</param>
    public BaseReservationAuthorizeAttribute(params RoleApplication[] roles)
    {
        var allowedRolesAsStrings = roles.Select(x => StringExtension.Capitalize(Enum.GetName(typeof(RoleApplication), x)!));
        Roles = string.Join(",", allowedRolesAsStrings);
    }
}
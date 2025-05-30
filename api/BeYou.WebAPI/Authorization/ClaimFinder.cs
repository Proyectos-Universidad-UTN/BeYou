using System.Security.Claims;

namespace BeYou.WebAPI.Authorization;

public class ClaimFinder(IEnumerable<Claim> claims)
{
    /// <summary>
    /// User id property
    /// </summary>
    /// <returns></returns>
    public Claim? UserId { get => claims.FirstOrDefault(m => m.Type == "UserId"); }

    /// <summary>
    /// Email property
    /// </summary>
    /// <returns></returns>
    public Claim? Email { get => claims.FirstOrDefault(m => m.Type == "Email"); }

    /// <summary>
    /// Role property
    /// </summary>
    /// <returns></returns>
    public Claim? Role { get => claims.FirstOrDefault(m => m.Type == ClaimTypes.Role); }
}
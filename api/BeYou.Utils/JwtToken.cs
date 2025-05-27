using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BeYou.Utils;

public static class JwtToken
{
    /// <summary>
    /// Get JWT Security token object
    /// </summary>
    /// <param name="jwt">JWT to be readed</param>
    /// <returns>JwtSecurityToken</returns>
    private static JwtSecurityToken ConvertJwtStringToJwtSecurityToken(string? jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);

        return token;
    }

    /// <summary>
    /// Get key id, audience and claims from jwt
    /// </summary>
    /// <param name="jwt">JWT to be readed</param>
    /// <returns>(string, List<string>, IEnumerable<Claim>)</returns>
    public static (string keyId, List<string> audience, IEnumerable<Claim> claims) DecodeJwt(string? jwt)
    {
        var token = ConvertJwtStringToJwtSecurityToken(jwt);

        var keyId = token.Header.Kid;
        var audience = token.Audiences.ToList();
        var claims = token.Claims;

        return (keyId, audience, claims);
    }
}

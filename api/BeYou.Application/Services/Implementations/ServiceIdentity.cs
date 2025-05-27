using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Application.Dtos.Response.Authentication;
using BeYou.Application.Configuration.Authentication;
using BeYou.Domain.Models;

namespace BeYou.Application.Services.Implementations;

public class ServiceIdentity(AuthenticationConfiguration authenticationConfiguration, ICoreService<TokenMaster> coreService,
                                IServiceUser serviceUser, TokenValidationParameters tokenValidationParameters) : IServiceIdentity
{
    /// <inheritdoc />    
    public async Task<TokenModel> LoginAsync(RequestUserLoginDto login)
    {
        string md5Password = Hashing.HashMd5(login.Password);

        var loginUser = await serviceUser.LoginAsync(login.Email, md5Password);

        return await AuthenticateAsync(loginUser);
    }

    /// <inheritdoc />
    public async Task<TokenModel> RefreshTokenAsync(TokenModel request)
    {
        var response = new AuthenticationResult();
        var authResponse = await GetRefreshTokenAsync(request.Token, request.RefreshToken);
        if (!authResponse.Success) throw new BaseReservationException("No se pudo obtener el token actualizado");

        response.Token = authResponse.Token;
        response.RefreshToken = authResponse.RefreshToken;

        return response;
    }

    /// <summary>
    /// Generate the token from a valid authentication
    /// </summary>
    /// <param name="user">User information</param>
    /// <returns>AuthenticationResult</returns>
    private async Task<AuthenticationResult> AuthenticateAsync(ResponseUserDto user)
    {
        var authenticationResult = new AuthenticationResult();
        var tokenHandler = new JwtSecurityTokenHandler();

        ClaimsIdentity subject = GenerateClaims(user);

        var tokenDescriptor = GetSecurityTokenDescriptor(subject);

        var token = tokenHandler.CreateToken(tokenDescriptor);
        authenticationResult.Token = tokenHandler.WriteToken(token);

        var refreshToken = GenerateTokenMaster(token.Id, user.Id);
        refreshToken.CreatedBy = user.Email;

        var tokenMaster = await coreService.UnitOfWork.Repository<TokenMaster>().AddAsync(refreshToken);
        await coreService.UnitOfWork.SaveChangesAsync();
        if (tokenMaster == null) throw new NotFoundException("Token no almacenado");

        authenticationResult.RefreshToken = refreshToken.Token;
        authenticationResult.Success = true;

        return authenticationResult;
    }

    /// <summary>
    /// Generate all the claims needed on the JWT
    /// </summary>
    /// <param name="user">User information</param>
    /// <returns>ClaimsIdentity</returns>
    private static ClaimsIdentity GenerateClaims(ResponseUserDto user)
    {
        return new ClaimsIdentity(new Claim[]
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
            new Claim("FullName", $"{user.FirstName} {user.LastName}"),
            new Claim("Email", user.Email),
            new Claim(ClaimTypes.Role, user.Role.Description),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        });
    }

    /// <summary>
    /// Get security token descriptor for claims identity
    /// </summary>
    /// <param name="subject">Claims identity subject</param>
    /// <returns>SecurityTokenDescriptor</returns>
    private SecurityTokenDescriptor GetSecurityTokenDescriptor(ClaimsIdentity subject) =>
        new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = DateTime.UtcNow.Add(authenticationConfiguration.JwtSettings_TokenLifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationConfiguration.JwtSettings_Secret)), SecurityAlgorithms.HmacSha256Signature)
        };

    /// <summary>
    /// Generate element that contains information to be saved on the database
    /// </summary>
    /// <param name="tokenId">Token id</param>
    /// <param name="userId">User id</param>
    /// <returns>TokenMaster</returns>
    private TokenMaster GenerateTokenMaster(string tokenId, long userId) =>
        new TokenMaster
        {
            Token = Guid.NewGuid().ToString(),
            JwtId = tokenId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            Used = false,
            ExpireAt = DateTime.UtcNow.AddMonths(6)
        };

    /// <summary>
    /// Get the refresh token information
    /// </summary>
    /// <param name="token">Jwt token</param>
    /// <param name="refreshToken">Jwt refresh token</param>
    /// <returns>AuthenticationResult</returns>
    private async Task<AuthenticationResult> GetRefreshTokenAsync(string token, string refreshToken)
    {
        var validatedToken = GetPrincipalFromToken(token);
        if (validatedToken == null) return new AuthenticationResult { Errors = new[] { "Token Inválido" } };

        var expiryDateTimeUtc = DateTime.UnixEpoch
            .AddSeconds(long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value));

        if (expiryDateTimeUtc > DateTime.UtcNow) return new AuthenticationResult { Errors = new[] { "Token aun no ha expirado" } };

        var spec = new BaseSpecification<TokenMaster>(x => x.Token == refreshToken);
        if (await coreService.UnitOfWork.Repository<TokenMaster>().FirstOrDefaultAsync(spec) == null) throw new NotFoundException("Token no encontrado.");

        var existingRefreshToken = await coreService.UnitOfWork.Repository<TokenMaster>().FirstOrDefaultAsync(spec);

        if (existingRefreshToken == null) return new AuthenticationResult { Errors = new[] { "Token no existe" } };
        if (DateTime.UtcNow > existingRefreshToken.ExpireAt) return new AuthenticationResult { Errors = new[] { "El token de actualización ya expiró" } };
        if (existingRefreshToken.Used) return new AuthenticationResult { Errors = new[] { "El token de actualización ya ha sido usado" } };
        if (existingRefreshToken.JwtId != validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value) return new AuthenticationResult { Errors = new[] { "El token de actualización no coincide con el JWt" } };

        existingRefreshToken.Used = true;

        coreService.UnitOfWork.Repository<TokenMaster>().Update(existingRefreshToken);
        await coreService.UnitOfWork.SaveChangesAsync();

        var user = await GetUserAsync(validatedToken.Claims.Single(x => x.Type == "UserId").Value);

        return await AuthenticateAsync(user);
    }

    /// <summary>
    /// Get user with specific id
    /// </summary>
    /// <param name="userId">Id to look for</param>
    /// <returns>Usuario</returns>
    private async Task<ResponseUserDto> GetUserAsync(string userId)
    {
        short userIdParsed;
        short.TryParse(userId, out userIdParsed);

        return await serviceUser.FindByIdAsync(userIdParsed);
    }

    /// <summary>
    /// Get Claim from jwt token
    /// </summary>
    /// <param name="token">Jwt token</param>
    /// <returns>ClaimsPrincipal</returns>
    private ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var validationParameters = tokenValidationParameters.Clone();
            validationParameters.ValidateLifetime = false;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            if (!IsJwtWithValidSecurityAlgorithm(validatedToken)) return null!;

            return principal;
        }
        catch
        {
            return null!;
        }
    }

    /// <summary>
    /// Validate if jwt is using the correct security algorithm
    /// </summary>
    /// <param name="validatedToken">Jwt security token</param>
    /// <returns>bool</returns>
    private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken) => (validatedToken is JwtSecurityToken jwtSecurityToken) &&
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);
}


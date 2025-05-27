using BaseReservation.Application.Dtos.Request;
using BaseReservation.Application.Dtos.Response.Authentication;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceIdentity
{
    /// <summary>
    /// Authenticates a user based on the provided login credentials (email and password).
    /// </summary>
    /// <param name="login">The `RequestUserLoginDto` containing the user's email and password.</param>
    /// <returns>Returns a `TokenModel` containing the authentication token for the user.</returns>
    /// <exception cref="UnAuthorizedException">Thrown if the email or password is invalid.</exception>
    Task<TokenModel> LoginAsync(RequestUserLoginDto login);

    /// <summary>
    /// Refreshes the authentication token using the provided `TokenModel` containing the current token and refresh token.
    /// </summary>
    /// <param name="request">The `TokenModel` containing the current token and refresh token.</param>
    /// <returns>Returns a new `TokenModel` with the refreshed token and refresh token.</returns>
    /// <exception cref="ArtInkException">Thrown if the token refresh process fails.</exception>

    Task<TokenModel> RefreshTokenAsync(TokenModel request);
}

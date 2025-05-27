using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces.Authorization;

public interface IServiceUserAuthorization
{
    /// <summary>
    /// Get logged user from context jwt
    /// </summary>
    /// <returns>ResponseUsuarioDto</returns>
    Task<ResponseUserDto> GetLoggedUser();
}

using AutoMapper;
using BeYou.Application.Services.Interfaces.Authorization;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Exceptions;

namespace BeYou.Application.Services.Implementations.Authorization;

public class ServiceUserAuthorization(IServiceUserContext serviceUserContext, IServiceUser serviceUser, IMapper mapper) : IServiceUserAuthorization
{
    /// <inheritdoc />
    public async Task<ResponseUserDto> GetLoggedUser()
    {
        var existingUser = await serviceUser.FindByEmailAsync(serviceUserContext.UserId!);
        var user = existingUser ?? throw new NotFoundException("No existe el usuario");
        return mapper.Map<ResponseUserDto>(user);
    }
}

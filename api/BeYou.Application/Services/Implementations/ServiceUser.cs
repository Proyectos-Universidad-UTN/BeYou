using AutoMapper;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;

namespace BeYou.Application.Services.Implementations;

public class ServiceUser(ICoreService<User> coreService, IMapper mapper) : IServiceUser
{
    private readonly string[] UserWithRole = ["RoleIdNavigation"];

    /// <inheritdoc />
    public async Task<ResponseUserDto> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<User>(x => x.Id == id);
        var user = await coreService.UnitOfWork.Repository<User>().FirstOrDefaultAsync(spec, UserWithRole);
        if (user == null) throw new NotFoundException("Usuario no encontrado.");

        return mapper.Map<ResponseUserDto>(user);
    }

    /// <inheritdoc />
    public async Task<ResponseUserDto> FindByEmailAsync(string email)
    {
        var spec = new BaseSpecification<User>(x => x.Email == email);
        var user = await coreService.UnitOfWork.Repository<User>().FirstOrDefaultAsync(spec);
        if (user == null) throw new NotFoundException("Usuario no encontrado.");

        return mapper.Map<ResponseUserDto>(user);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseUserDto>> ListAllAsync(string? role = null)
    {
        if (role == null)
        {
            var list = await coreService.UnitOfWork.Repository<User>().ListAllAsync();
            return mapper.Map<ICollection<ResponseUserDto>>(list);
        }

        RoleApplication roleEnum;
        if (!Enum.TryParse(role, out roleEnum)) throw new BaseReservationException("Rol Inválido");

        var spec = new BaseSpecification<User>(x => x.RoleId == (long)roleEnum);
        var listFilter = await coreService.UnitOfWork.Repository<User>().ListAsync(spec);
        var collection = mapper.Map<ICollection<ResponseUserDto>>(listFilter);

        return collection;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsUserAsync(long id)
    {
        return await coreService.UnitOfWork.Repository<User>().ExistsAsync(id);
    }

    /// <inheritdoc />
    public async Task<ResponseUserDto> LoginAsync(string email, string password)
    {
        var spec = new BaseSpecification<User>(x => x.Email == email && x.Password == password && x.Active);
        var user = await coreService.UnitOfWork.Repository<User>().FirstOrDefaultAsync(spec, UserWithRole);
        if (user == null) throw new NotFoundException("Email o contraseña incorrecta.");

        return mapper.Map<ResponseUserDto>(user);
    }
}

using AutoMapper;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;

namespace BeYou.Application.Services.Implementations;

public class ServiceRole(ICoreService<Role> coreService, IMapper mapper) : IServiceRole
{
    /// <inheritdoc />
    public async Task<ResponseRoleDto> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Role>().ExistsAsync(id)) throw new NotFoundException("Rol no encontrado.");

        var spec = new BaseSpecification<Role>(x => x.Id == id);
        var role = await coreService.UnitOfWork.Repository<Role>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseRoleDto>(role);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseRoleDto>> ListAllAsync()
    {
        var roles = await coreService.UnitOfWork.Repository<Role>().ListAllAsync();
        return mapper.Map<ICollection<ResponseRoleDto>>(roles);
    }
}

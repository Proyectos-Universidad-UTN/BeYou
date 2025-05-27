using AutoMapper;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;

namespace BeYou.Application.Services.Implementations;

public class ServiceTypeService(ICoreService<TypeService> coreService, IMapper mapper) : IServiceTypeService
{
    /// <inheritdoc />
    public async Task<ResponseTypeServiceDto> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<TypeService>().ExistsAsync(id)) throw new NotFoundException("Tipo de servicio no encontrado.");

        var spec = new BaseSpecification<TypeService>(x => x.Id == id);
        var typeService = await coreService.UnitOfWork.Repository<TypeService>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseTypeServiceDto>(typeService);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseTypeServiceDto>> ListAllAsync()
    {
        var typesService = await coreService.UnitOfWork.Repository<TypeService>().ListAllAsync();
        return mapper.Map<ICollection<ResponseTypeServiceDto>>(typesService);
    }
}

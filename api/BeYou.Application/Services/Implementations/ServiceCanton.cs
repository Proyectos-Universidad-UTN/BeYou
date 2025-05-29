using AutoMapper;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;


namespace BeYou.Application.Services.Implementations;

public class ServiceCanton(ICoreService<Canton> coreService, IMapper mapper) : IServiceCanton
{
    /// <inheritdoc />
    public async Task<ICollection<ResponseCantonDto>> ListAllByProvinceAsync(long provinceId)
    {
        var spec = new BaseSpecification<Canton>(x => x.ProvinceId == provinceId);
        var list = await coreService.UnitOfWork.Repository<Canton>().ListAsync(spec);
        var collection = mapper.Map<ICollection<ResponseCantonDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ResponseCantonDto> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Canton>().ExistsAsync(id)) throw new NotFoundException("Cantón no encontrado.");

        var spec = new BaseSpecification<Canton>(x => x.Id == id);
        var canton = await coreService.UnitOfWork.Repository<Canton>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseCantonDto>(canton);
    }
}
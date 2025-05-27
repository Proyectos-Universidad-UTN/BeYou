using AutoMapper;
using BeYou.Infrastructure;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;

namespace BeYou.Application.Services.Implementations;

public class ServiceProvince(ICoreService<Province> coreService, IMapper mapper) : IServiceProvince
{
    /// <inheritdoc />
    public async Task<ResponseProvinceDto> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Province>().ExistsAsync(id)) throw new NotFoundException("Provincia no encontrada.");

        var spec = new BaseSpecification<Province>(x => x.Id == id);
        var province = await coreService.UnitOfWork.Repository<Province>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseProvinceDto>(province);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseProvinceDto>> ListAllAsync()
    {
        var provinces = await coreService.UnitOfWork.Repository<Province>().ListAllAsync();
        return mapper.Map<ICollection<ResponseProvinceDto>>(provinces);
    }
}

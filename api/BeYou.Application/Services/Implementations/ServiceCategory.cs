using AutoMapper;
using BeYou.Domain.Exceptions;
using BeYou.Application.Dtos.Response;
using BeYou.Domain.Core.Specifications;
using BeYou.Application.Core.Interfaces;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Models;

namespace BeYou.Application.Services.Implementations;

public class ServiceCategory(ICoreService<Category> coreService, IMapper mapper) : IServiceCategory
{
    /// <inheritdoc />
    public async Task<ResponseCategoryDto> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Category>().ExistsAsync(id)) throw new NotFoundException("Categoría no encontrada.");

        var spec = new BaseSpecification<Category>(x => x.Id == id);
        var category = await coreService.UnitOfWork.Repository<Category>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseCategoryDto>(category);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseCategoryDto>> ListAllAsync()
    {
        var list = await coreService.UnitOfWork.Repository<Category>().ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseCategoryDto>>(list);

        return collection;
    }
}

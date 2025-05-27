using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceCategory
{
    /// <summary>
    /// Finds a category by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>ResponseCategoryDto</returns>
    /// <exception cref="NotFoundException">Thrown when no category is found with the specified ID.</exception>
    Task<ICollection<ResponseCategoryDto>> ListAllAsync();

    /// <summary>
    /// Get a list of all categories
    /// </summary>
    /// <returns>ICollection of ResponseCategoryDto</returns>
    Task<ResponseCategoryDto> FindByIdAsync(long id);
}

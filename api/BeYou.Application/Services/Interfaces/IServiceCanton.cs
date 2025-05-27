using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceCanton
{
    /// <summary>
    /// Retrieves a list of all cantons by the given province ID.
    /// </summary>
    /// <param name="provinceId">The ID of the province to filter cantons.</param>
    /// <returns>ICollection of ResponseCantonDto</returns>
    Task<ICollection<ResponseCantonDto>> ListAllByProvinceAsync(long provinceId);

    /// <summary>
    /// Finds a canton by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the canton to retrieve. </param>
    /// <returns>ResponseCantonDto</returns>
    /// <exception cref="NotFoundException">Thrown when no canton is found with the specified ID</exception>
    Task<ResponseCantonDto> FindByIdAsync(long id);
}

using BaseReservation.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceProvince
{
    /// <summary>
    /// Get list of all provinces
    /// </summary>
    /// <returns>ICollection of ResponseProvinceDto</returns>
    Task<ICollection<ResponseProvinceDto>> ListAllAsync();

    /// <summary>
    /// Get province with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseProvinceDto</returns>
    Task<ResponseProvinceDto> FindByIdAsync(long id);
}

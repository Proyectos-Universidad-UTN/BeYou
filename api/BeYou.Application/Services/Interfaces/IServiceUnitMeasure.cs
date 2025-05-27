using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceUnitMeasure
{
    /// <summary>
    /// Get list of all units of measurement
    /// </summary>
    /// <returns>ICollection of ResponseUnitMeasureDto</returns>
    Task<ICollection<ResponseUnitMeasureDto>> ListAllAsync();

    /// <summary>
    /// Get unit of measurement with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseUnitMeasureDto</returns>
    Task<ResponseUnitMeasureDto> FindByIdAsync(long id);

    /// <summary>
    /// Create a new unit of measurement
    /// </summary>
    /// <param name="requestUnitMeasureDto">RequestUnitMeasureDto object with the data to create a new unit of measurement</param>
    /// <returns>ResponseUnitMeasureDto object with the data of the created unit of measurement </returns>
    Task<ResponseUnitMeasureDto> CreateUnitMeasureAsync(RequestUnitMeasureDto requestUnitMeasureDto);

    /// <summary>
    /// Update an existing unit of measurement 
    /// </summary>
    /// <param name="id">
    /// Id of the unit of measurement to update
    /// </param>
    /// <param name="requestUnitMeasureDto">RequestUnitMeasureDto object with the data to update the unit of measurement</param>
    /// <returns>ResponseUnitMeasureDto object with the data of the updated unit of measurement</returns>
    Task<ResponseUnitMeasureDto> UpdateUnitMeasureAsync(long id, RequestUnitMeasureDto requestUnitMeasureDto);

    /// <summary>
    /// Delete an existing unit of measurement
    /// </summary>
    /// <param name="id">
    /// Id of the unit of measurement to delete
    /// </param>
    /// <returns>
    /// True if the unit of measurement was deleted, false otherwise
    /// </returns>
    Task<bool> DeleteUnitMeasureAsync(long id);
}

using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceHoliday
{
    /// <summary>
    ///  Creates a new holiday
    /// </summary>
    /// <param name="holidayDto">The data transfer object containing the information of the Factura to create</param>
    /// <returns>RequestHolidayDto</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseHolidayDto> CreateHolidayAsync(RequestHolidayDto holidayDto);

    /// <summary>
    /// Deletes a holiday  by its identifier if it exists in the repository.
    /// </summary>
    /// <param name="id">The identifier of the holiday to delete.</param>
    /// <returns>>Returns `true` if the holiday was successfully deleted, otherwise `false`</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<bool> DeleteHolidayAsync(long id);

    /// <summary>
    /// Finds a feriado by its unique ID.
    /// </summary>
    /// <param name="id">The identifier of the holiday to retrieve.</param>
    /// <returns>ResponseHolidayDto</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseHolidayDto> FindByIdAsync(long id);

    /// <summary>
    /// Get a list of all holidays
    /// </summary>
    /// <returns>ICollection of ResponseHolidayDto</returns>
    Task<ICollection<ResponseHolidayDto>> ListAllAsync();

    /// <summary>
    /// Updates an existing holiday identified by its ID with the provided data.
    /// </summary>
    /// <param name="id">The identifier of the holiday to update.</param>
    /// <param name="holidayDto">The data transfer object containing the updated holiday information.</param>
    /// <returns>RequestHolidayDto</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseHolidayDto> UpdateHolidayAsync(long id, RequestHolidayDto holidayDto);
}

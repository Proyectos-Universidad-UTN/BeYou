using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceSchedule
{
    /// <summary>
    ///  Creates a new schedule
    /// </summary>
    /// <param name="scheduleDto">Schedule request model to be added</param>
    /// <returns>ResponseScheduleDto</returns>
    Task<ResponseScheduleDto> CreateScheduleAsync(RequestScheduleDto scheduleDto);

    /// <summary>
    /// Updates an existing schedule
    /// </summary>
    /// <param name="id">Schedule id to identity record</param>
    /// <param name="scheduleDto">Schedule request model to be updated</param>
    /// <returns>ResponseScheduleDto</returns>
    Task<ResponseScheduleDto> UpdateScheduleAsync(long id, RequestScheduleDto scheduleDto);

    /// <summary>
    /// Get list of all ResponseScheduleDto.
    /// </summary>
    /// <returns>ICollection of ResponseScheduleDto</returns>
    Task<ICollection<ResponseScheduleDto>> ListAllAsync();

    /// <summary>
    /// Finds a schedule by its unique identifier
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseScheduleDto</returns>
    Task<ResponseScheduleDto> FindByIdAsync(long id);

    /// <summary>
    /// Delete existing schedule
    /// </summary>
    /// <param name="id">Schedule id to look for</param>
    /// <returns>bool</returns>
    Task<bool> DeleteScheduleAsync(long id);
}

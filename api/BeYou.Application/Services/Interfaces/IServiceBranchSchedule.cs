using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Enums;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceBranchSchedule
{
    /// <summary>
    /// Get list of all branch's schedules by branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <returns>ICollection of ResponseBranchScheduleDto</returns>
    Task<ICollection<ResponseBranchScheduleDto>> ListAllByBranchAsync(long branchId);

    /// <summary>
    /// Find all branch's schedules by week day
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="weekDay">Week day</param>
    /// <returns>ICollection of ResponseBranchScheduleDto</returns>
    Task<ResponseBranchScheduleDto> FindByWeekDayAsync(long branchId, WeekDayApplication weekDay);

    /// <summary>
    /// Get branch schedule with specific id
    /// </summary>
    /// <param name="id">Branch scheduel id to look for</param>
    /// <returns>ResponseBranchScheduleDto</returns>
    Task<ResponseBranchScheduleDto?> FindByIdAsync(long id);

    /// <summary>
    /// Create branch's schedules
    /// </summary>
    /// <param name="branchId">Branch id that receive schedules</param>
    /// <param name="branchSchedules">List of Branch's schedules will be added</param>
    /// <returns>bool</returns>
    Task<bool> CreateBranchScheduleAsync(long branchId, IEnumerable<RequestBranchScheduleDto> branchSchedules);
}

using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceBranchHoliday
{
    /// <summary>
    /// Get list of all holiday's branch by year
    /// </summary>
    /// <param name="branchId">Branch id to look for</param>
    /// <param name="year">Year to look for/param>
    /// <returns>ICollection of ResponseBranchHolidayDto</returns>
    Task<ICollection<ResponseBranchHolidayDto>> ListAllByBranchAsync(long branchId, short? year);

    /// <summary>
    /// Get list of all holiday's branch by year
    /// </summary>
    /// <param name="branchId">Branch id to look for</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>ICollection of ResponseBranchHolidayDto</returns>
    Task<ICollection<ResponseBranchHolidayDto>> ListAllByBranchAsync(long branchId, DateOnly startDate, DateOnly endDate);

    /// <summary>
    /// Get Branch holiday with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseBranchHolidayDto</returns>
    Task<ResponseBranchHolidayDto> FindByIdAsync(long id);

    /// <summary>
    /// Create branch's holidays
    /// </summary>
    /// <param name="branchId">Branch id to receive holidays</param>
    /// <param name="branchHolidays">List of branch holiday request mode to be added</param>
    /// <returns>bool</returns>
    Task<bool> CreateBranchHolidaysAsync(long branchId, IEnumerable<RequestBranchHolidayDto> branchHolidays);
}

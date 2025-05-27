using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceUserBranch
{
    /// <summary>
    /// Create branch's users 
    /// </summary>
    /// <param name="branchId">Branch id that receives list of users</param>
    /// <param name="branchUsers">List of branch's users</param>
    /// <returns>bool</returns>
    Task<bool> CreateUserBranchAsync(long branchId, IEnumerable<RequestUserBranchDto> branchUsers);

    // <summary>
    /// Validate if the user can be assigned to another branch
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="branchId">Branch to be assigned</param>
    /// <returns>True if is available, if not, false</returns>
    Task<bool> IsAvailableAsync(long userId, long branchId);
}

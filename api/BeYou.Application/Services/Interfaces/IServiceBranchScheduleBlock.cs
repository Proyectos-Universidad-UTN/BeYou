using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceBranchScheduleBlock
{
    /// <summary>
    /// Get list of all blocks by branch schedule
    /// </summary>
    /// <param name="branchScheduleId">Branch schedule id</param>
    /// <returns>ICollection of ResponseBranchScheduleBlockDto</returns>
    Task<ICollection<ResponseBranchScheduleBlockDto>> ListAllByBranchScheduleAsync(long branchScheduleId);

    /// <summary>
    /// Get list of all blocks by branch schedule
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <returns>ICollection of ResponseBranchScheduleBlockDto</returns>
    Task<ICollection<ResponseBranchScheduleBlockDto>> ListAllByBranchAsync(long branchId);

    /// <summary>
    /// Get Branch schedule block with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseBranchScheduleBlockDto</returns>
    Task<ResponseBranchScheduleBlockDto> FindByIdAsync(long id);

    /// <summary>
    /// Create branch schedule block
    /// </summary>
    /// <param name="branchScheduleBlock">Branch schedule block request model to be added</param>
    /// <returns>ResponseBranchScheduleBlockDto</returns>
    Task<ResponseBranchScheduleBlockDto> CreateBranchScheduleBlockAsync(RequestBranchScheduleBlockDto branchScheduleBlock);

    /// <summary>
    /// Create branch schedule's blocks
    /// </summary>
    /// <param name="branchScheduleId">Branch schedule id that receive blocks</param>
    /// <param name="branchScheduleBlocks">List of Branch schedule's blocks will be added</param>
    /// <returns>bool</returns>
    Task<bool> CreateBranchScheduleBlockAsync(long branchScheduleId, IEnumerable<RequestBranchScheduleBlockDto> branchScheduleBlocks);

    /// <summary>
    /// Update branch schedule block
    /// </summary>
    /// <param name="id">Branch schedule block id to identiy the record</param>
    /// <param name="branchScheduleBlock">Branch schedule block request model to be updated</param>
    /// <returns>ResponseBranchScheduleBlockDto</returns>
    Task<ResponseBranchScheduleBlockDto> UpdateBranchScheduleBlockAsync(long id, RequestBranchScheduleBlockDto branchScheduleBlock);

    /// <summary>
    /// Deletes a branch schedule block based on the provided Id.
    /// </summary>
    /// <param name="id">Id of the block to delete.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> DeleteBranchScheduleBlockAsync(long id);
}
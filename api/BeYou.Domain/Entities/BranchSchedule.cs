using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;
public partial class BranchSchedule : BaseSimpleDto
{
    public long BranchId { get; set; }

    public long ScheduleId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<BranchScheduleBlock> BranchScheduleBlocks { get; set; } = new List<BranchScheduleBlock>();

    public virtual Schedule Schedule { get; set; } = null!;
}
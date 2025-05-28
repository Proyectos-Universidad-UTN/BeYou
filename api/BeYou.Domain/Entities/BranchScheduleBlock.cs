using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class BranchScheduleBlock : BaseSimpleDto
{
    public long BranchScheduleId { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public bool Active { get; set; }

    public virtual BranchSchedule BranchSchedule { get; set; } = null!;
}
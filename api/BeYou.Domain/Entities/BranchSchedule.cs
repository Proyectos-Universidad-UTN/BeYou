using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class BranchSchedule
{
    public long Id { get; set; }

    public long BranchId { get; set; }

    public long ScheduleId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<BranchScheduleBlock> BranchScheduleBlocks { get; set; } = new List<BranchScheduleBlock>();

    public virtual Schedule Schedule { get; set; } = null!;
}

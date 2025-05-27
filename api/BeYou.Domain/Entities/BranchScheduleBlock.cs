using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class BranchScheduleBlock
{
    public long Id { get; set; }

    public long BranchScheduleId { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public bool Active { get; set; }

    public virtual BranchSchedule BranchSchedule { get; set; } = null!;
}

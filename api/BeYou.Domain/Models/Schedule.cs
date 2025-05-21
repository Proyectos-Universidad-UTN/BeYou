using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class Schedule
{
    public long Id { get; set; }

    public string Day { get; set; } = null!;

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<BranchSchedule> BranchSchedules { get; set; } = new List<BranchSchedule>();
}

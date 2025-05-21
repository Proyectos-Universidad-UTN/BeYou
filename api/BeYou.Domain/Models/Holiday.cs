using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class Holiday
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Month { get; set; } = null!;

    public byte Day { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<BranchHoliday> BranchHolidays { get; set; } = new List<BranchHoliday>();
}

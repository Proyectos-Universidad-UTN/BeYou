using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class BranchHoliday
{
    public long Id { get; set; }

    public long HolidayId { get; set; }

    public long BranchId { get; set; }

    public short Year { get; set; }

    public DateOnly Date { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Holiday Holiday { get; set; } = null!;
}

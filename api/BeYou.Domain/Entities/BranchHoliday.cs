using BeYou.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BeYou.Domain.Models;

[Table("BranchHoliday")]
[Index("HolidayId", Name = "IX_BranchHoliday_HolidayId")]
[Index("BranchId", Name = "IX_BranchHoliday_BranchId")]
public partial class BranchHoliday : BaseSimpleDto
{
    public long HolidayId { get; set; }

    public long BranchId { get; set; }

    public short Year { get; set; }

    public DateOnly Date { get; set; }

    [ForeignKey("HolidayId")]
    [InverseProperty("BranchHolidays")]
    public virtual Holiday HolidayIdNavigation { get; set; } = null!;

    [ForeignKey("BranchId")]
    [InverseProperty("BranchHolidays")]
    public virtual Branch BranchIdNavigation { get; set; } = null!;
}

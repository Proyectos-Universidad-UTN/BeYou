using BeYou.Domain.Core.Models;
using BeYou.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Models;

[Table("Holiday")]
public partial class Holiday : BaseEntity
{
    [StringLength(80)]
    public string Name { get; set; } = null!;

    [StringLength(25)]
    public Month Month { get; set; }

    public byte Day { get; set; }

    [InverseProperty("HolidayIdNavigation")]
    public virtual ICollection<BranchHoliday> BranchHolidays { get; set; } = new List<BranchHoliday>();
}

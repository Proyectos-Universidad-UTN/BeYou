using BeYou.Domain.Core.Models;
using BeYou.Domain.Enums;

namespace BeYou.Domain.Models;

public partial class Holiday : BaseEntity
{
    public string Name { get; set; } = null!;

    public Month Month { get; set; }

    public byte Day { get; set; }

    public virtual ICollection<BranchHoliday> BranchHolidays { get; set; } = new List<BranchHoliday>();
}
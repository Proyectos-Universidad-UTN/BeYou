using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Holiday : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Month { get; set; } = null!;

    public byte Day { get; set; }

    public virtual ICollection<BranchHoliday> BranchHolidays { get; set; } = new List<BranchHoliday>();
}
using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Schedule : BaseEntity
{
    public string Day { get; set; } = null!;

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public virtual ICollection<BranchSchedule> BranchSchedules { get; set; } = new List<BranchSchedule>();
}
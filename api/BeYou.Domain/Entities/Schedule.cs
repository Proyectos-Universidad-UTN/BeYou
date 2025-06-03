using BeYou.Domain.Core.Models;
using BeYou.Domain.Enums;

namespace BeYou.Domain.Models;

public partial class Schedule : BaseEntity
{
    public WeekDay Day { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public virtual ICollection<BranchSchedule> BranchSchedules { get; set; } = new List<BranchSchedule>();
}
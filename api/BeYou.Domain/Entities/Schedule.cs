using BeYou.Domain.Core.Models;
using BeYou.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeYou.Domain.Models;

[Table("Schedule")]
public partial class Schedule : BaseEntity
{
    public WeekDay Day { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    [InverseProperty("ScheduleIdNavigation")]
    public virtual ICollection<BranchSchedule> BranchSchedules { get; set; } = new List<BranchSchedule>();
}
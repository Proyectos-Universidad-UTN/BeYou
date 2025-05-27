using BeYou.Application.Dtos.Response.Base;
using BeYou.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeYou.Application.Dtos.Response;

public record ResponseScheduleDto : BaseEntity
{
    public WeekDayApplication Day { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public virtual ICollection<ResponseBranchScheduleDto> BranchSchedules { get; set; } = new List<ResponseBranchScheduleDto>();
}

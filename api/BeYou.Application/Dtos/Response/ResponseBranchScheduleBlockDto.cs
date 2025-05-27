using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseBranchScheduleBlockDto : BaseSimpleEntity
{
    public long BranchScheduleId { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public bool Active { get; set; }

    public virtual ResponseBranchScheduleDto BranchSchedule { get; set; } = null!;
}

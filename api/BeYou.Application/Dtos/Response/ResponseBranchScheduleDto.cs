using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseBranchScheduleDto : BaseSimpleEntity
{
    public long BranchId { get; set; }

    public long ScheduleId { get; set; }

    public virtual ResponseScheduleDto Schedule { get; set; } = null!;

    public virtual ResponseBranchDto Branch { get; set; } = null!;

    public virtual ICollection<ResponseBranchScheduleBlockDto> BranchScheduleBlocks { get; set; } = new List<ResponseBranchScheduleBlockDto>();
}
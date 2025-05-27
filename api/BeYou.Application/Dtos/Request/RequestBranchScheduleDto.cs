namespace BeYou.Application.Dtos.Request;

public record RequestBranchScheduleDto
{
    public long BranchId { get; set; }

    public long ScheduleId { get; set; }
}

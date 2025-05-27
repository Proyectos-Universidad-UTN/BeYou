namespace BeYou.Application.Dtos.Request;

public class RequestBranchScheduleBlockDto
{
    public long BranchScheduleId { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }
}

namespace BeYou.Application.Dtos.Request;

public record RequestBranchHolidayDto
{
    public long HolidayId { get; set; }

    public long BranchId { get; set; }

    public DateOnly Date { get; set; }

    public short Year { get; set; }
}
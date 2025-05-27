namespace BeYou.Application.Dtos.Request;

public record RequestReservationDto : RequestBaseDto
{
    public DateOnly Date { get; set; }

    public TimeOnly Hour { get; set; }

    public long BranchId { get; set; }

    public long CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public List<RequestReservationQuestionDto> ReservationQuestion { get; set; } = null!;

    public List<RequestReservationDetailDto> ReservationDetails { get; set; } = null!;
}

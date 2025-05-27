namespace BeYou.Application.Dtos.Request;

public record RequestReservationDetailDto : RequestBaseDto
{
    public long ReservationId { get; set; }

    public long? ServiceId { get; set; }

    public long? ProductId { get; set; }
}

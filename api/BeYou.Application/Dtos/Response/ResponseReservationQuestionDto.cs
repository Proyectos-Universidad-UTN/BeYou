
using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseReservationQuestionDto : BaseEntity
{
    public long ReservationId { get; set; }

    public string Question { get; set; } = null!;

    public string? Answer { get; set; }

    public virtual ResponseReservationDto Reservation { get; set; } = null!;
}

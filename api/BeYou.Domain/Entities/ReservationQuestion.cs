using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class ReservationQuestion : BaseEntity
{
    public long ReservationId { get; set; }

    public string Question { get; set; } = null!;

    public string? Answer { get; set; }

    public virtual Reservation Reservation { get; set; } = null!;
}
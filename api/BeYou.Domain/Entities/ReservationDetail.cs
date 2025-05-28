using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class ReservationDetail : BaseSimpleDto
{
    public long ReservationId { get; set; }

    public long? ServiceId { get; set; }

    public long? ProductId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Reservation Reservation { get; set; } = null!;

    public virtual Service? Service { get; set; }
}

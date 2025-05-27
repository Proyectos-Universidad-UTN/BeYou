using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class ReservationDetail
{
    public long Id { get; set; }

    public long ReservationId { get; set; }

    public long? ServiceId { get; set; }

    public long? ProductId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Reservation Reservation { get; set; } = null!;

    public virtual Service? Service { get; set; }
}

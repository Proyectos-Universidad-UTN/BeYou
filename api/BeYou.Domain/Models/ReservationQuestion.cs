using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class ReservationQuestion
{
    public long Id { get; set; }

    public long ReservationId { get; set; }

    public string Question { get; set; } = null!;

    public string? Answer { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual Reservation Reservation { get; set; } = null!;
}

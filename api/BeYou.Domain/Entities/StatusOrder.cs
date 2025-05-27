using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class StatusOrder
{
    public long Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

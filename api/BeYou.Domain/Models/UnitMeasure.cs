using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class UnitMeasure
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

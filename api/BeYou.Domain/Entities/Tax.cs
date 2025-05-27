using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class Tax
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }

    public bool Active { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

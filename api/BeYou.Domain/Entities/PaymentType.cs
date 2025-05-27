using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class PaymentType
{
    public long Id { get; set; }

    public string Description { get; set; } = null!;

    public string ReferenceNumber { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

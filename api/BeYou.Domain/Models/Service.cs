using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class Service
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public long TypeServiceId { get; set; }

    public decimal Price { get; set; }

    public string? Observation { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    public virtual TypeService TypeService { get; set; } = null!;
}

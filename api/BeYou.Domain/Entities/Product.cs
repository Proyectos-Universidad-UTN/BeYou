using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public long CategoryId { get; set; }

    public decimal Price { get; set; }

    public string Sku { get; set; } = null!;

    public long UnitMeasureId { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<InventoryProduct> InventoryProducts { get; set; } = new List<InventoryProduct>();

    public virtual ICollection<InvoiceDetailProduct> InvoiceDetailProducts { get; set; } = new List<InvoiceDetailProduct>();

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual ICollection<OrderDetailProduct> OrderDetailProducts { get; set; } = new List<OrderDetailProduct>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    public virtual UnitMeasure UnitMeasure { get; set; } = null!;
}

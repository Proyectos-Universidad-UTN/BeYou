using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class InvoiceDetail
{
    public long Id { get; set; }

    public long InvoiceId { get; set; }

    public long? ServiceId { get; set; }

    public long? ProductId { get; set; }

    public byte LineNumber { get; set; }

    public short Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Tax { get; set; }

    public decimal Total { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual ICollection<InvoiceDetailProduct> InvoiceDetailProducts { get; set; } = new List<InvoiceDetailProduct>();

    public virtual Product? Product { get; set; }

    public virtual Service? Service { get; set; }
}

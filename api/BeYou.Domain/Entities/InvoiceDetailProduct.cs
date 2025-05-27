using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class InvoiceDetailProduct
{
    public long Id { get; set; }

    public long InvoiceDetailId { get; set; }

    public long ProductId { get; set; }

    public decimal Quantity { get; set; }

    public virtual InvoiceDetail InvoiceDetail { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}

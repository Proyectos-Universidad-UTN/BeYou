using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class OrderDetailProduct
{
    public long Id { get; set; }

    public long OrderDetailId { get; set; }

    public long ProductId { get; set; }

    public decimal Quantity { get; set; }

    public virtual OrderDetail OrderDetail { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}

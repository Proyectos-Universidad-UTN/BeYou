using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class InventoryProductTransaction
{
    public long Id { get; set; }

    public long InventoryProductId { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal Quantity { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual InventoryProduct InventoryProduct { get; set; } = null!;
}

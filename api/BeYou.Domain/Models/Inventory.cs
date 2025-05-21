using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class Inventory
{
    public long Id { get; set; }

    public long BranchId { get; set; }

    public string Name { get; set; } = null!;

    public string TypeInventory { get; set; } = null!;

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<InventoryProduct> InventoryProducts { get; set; } = new List<InventoryProduct>();
}

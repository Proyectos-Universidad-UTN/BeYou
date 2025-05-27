using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class InventoryProduct
{
    public long Id { get; set; }

    public long InventoryId { get; set; }

    public long ProductId { get; set; }

    public decimal Assignable { get; set; }

    public decimal Mininum { get; set; }

    public decimal Maximum { get; set; }

    public long IdProducto { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual Product IdProductoNavigation { get; set; } = null!;

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual ICollection<InventoryProductTransaction> InventoryProductTransactions { get; set; } = new List<InventoryProductTransaction>();
}

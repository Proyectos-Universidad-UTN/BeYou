using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Inventory : BaseEntity
{
    public long BranchId { get; set; }

    public string Name { get; set; } = null!;

    public string TypeInventory { get; set; } = null!;

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<InventoryProduct> InventoryProducts { get; set; } = new List<InventoryProduct>();
}
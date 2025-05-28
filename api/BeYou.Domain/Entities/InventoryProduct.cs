using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class InventoryProduct : BaseEntity
{
    public long InventoryId { get; set; }

    public long ProductId { get; set; }

    public decimal Assignable { get; set; }

    public decimal Mininum { get; set; }

    public decimal Maximum { get; set; }

    public long IdProducto { get; set; }
    public virtual Product IdProductoNavigation { get; set; } = null!;

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual ICollection<InventoryProductTransaction> InventoryProductTransactions { get; set; } = new List<InventoryProductTransaction>();
}
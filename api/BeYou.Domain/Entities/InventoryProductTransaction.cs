using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class InventoryProductTransaction : BaseEntity
{
    public long InventoryProductId { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal Quantity { get; set; }

    public virtual InventoryProduct InventoryProduct { get; set; } = null!;
}
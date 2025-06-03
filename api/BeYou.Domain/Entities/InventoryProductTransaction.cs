using BeYou.Domain.Core.Models;
using BeYou.Domain.Enums;

namespace BeYou.Domain.Models;

public partial class InventoryProductTransaction : BaseEntity
{
    public long InventoryProductId { get; set; }

    public TransactionTypeInventory TransactionType { get; set; }

    public decimal Quantity { get; set; }

    public virtual InventoryProduct InventoryProduct { get; set; } = null!;
}
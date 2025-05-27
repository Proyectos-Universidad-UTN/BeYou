using BeYou.Domain.Core.Models;
using BeYou.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace BeYou.Domain.Models;

[Table("InventoryProductTransaction")]
[Index("InventoryProductId", Name = "IX_InventoryProductTransaction_InventoryProductId")]
public partial class InventoryProductTransaction : BaseEntity
{
    public long InventoryProductId { get; set; }

    public TransactionTypeInventory TransactionType { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Quantity { get; set; }

    [ForeignKey("InventoryProductId")]
    [InverseProperty("InventoryProductTransactions")]
    public virtual InventoryProduct InventoryProductIdNavigation { get; set; } = null!;
}
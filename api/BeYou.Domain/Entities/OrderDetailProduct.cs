using BeYou.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeYou.Domain.Models;

[Table("OrderDetailProduct")]
[Index("OrderDetailId", Name = "IX_OrderDetailProduct_OrderDetailId")]
[Index("ProductId", Name = "IX_OrderDetailProduct_ProductId")]
public partial class OrderDetailProduct : BaseSimpleDto
{
    public long OrderDetailId { get; set; }

    public long ProductId { get; set; }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal Quantity { get; set; }

    [ForeignKey("OrderDetailId")]
    [InverseProperty("OrderDetailProducts")]
    public virtual OrderDetail OrderDetailIdNavigation { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderDetailProducts")]
    public virtual Product ProductIdNavigation { get; set; } = null!;
}
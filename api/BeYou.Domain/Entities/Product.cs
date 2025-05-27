using BeYou.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BeYou.Domain.Models;

[Table("Product")]
[Index("CategoryId", Name = "IX_Producto_CategoryId")]
[Index("UnitMeasureId", Name = "IX_Producto_UnitMeasureId")]
public partial class Product : BaseEntity
{
    [StringLength(70)]
    public string Name { get; set; } = null!;

    [StringLength(150)]
    public string Description { get; set; } = null!;

    [StringLength(50)]
    public string Brand { get; set; } = null!;

    public long CategoryId { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Column("SKU")]
    [StringLength(50)]
    public string Sku { get; set; } = null!;

    public long UnitMeasureId { get; set; }

    [InverseProperty("ProductIdNavigation")]
    public virtual ICollection<InvoiceDetailProduct> InvoiceDetailProducts { get; set; } = new List<InvoiceDetailProduct>();

    [InverseProperty("ProductIdNavigation")]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    [InverseProperty("ProductIdNavigation")]
    public virtual ICollection<OrderDetailProduct> OrderDetailProducts { get; set; } = new List<OrderDetailProduct>();

    [InverseProperty("ProductIdNavigation")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [InverseProperty("ProductIdNavigation")]
    public virtual ICollection<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category CategoryIdNavigation { get; set; } = null!;

    [ForeignKey("UnitMeasureId")]
    [InverseProperty("Products")]
    public virtual UnitMeasure UnitMeasureIdNavigation { get; set; } = null!;

    [InverseProperty("ProductIdNavigation")]
    public virtual ICollection<InventoryProduct> InventoryProducts { get; set; } = new List<InventoryProduct>();
}
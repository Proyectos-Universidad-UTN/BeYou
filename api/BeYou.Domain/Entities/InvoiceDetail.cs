using BeYou.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeYou.Domain.Models;

[Table("InvoiceDetail")]
[Index("InvoiceId", Name = "IX_InvoiceDetail_InvoiceId")]
[Index("ServiceId", Name = "IX_InvoiceDetail_ServiceId")]
public partial class InvoiceDetail : BaseSimpleDto
{
    public long InvoiceId { get; set; }

    public long? ServiceId { get; set; }

    public long? ProductId { get; set; }

    public byte LineNumber { get; set; }

    public short Quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "money")]
    public decimal Tax { get; set; }

    [Column(TypeName = "money")]
    public decimal Total { get; set; }

    [InverseProperty("InvoiceDetailIdNavigation")]
    public virtual ICollection<InvoiceDetailProduct> InvoiceDetailProducts { get; set; } = new List<InvoiceDetailProduct>();

    [ForeignKey("InvoiceId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Invoice InvoiceIdNavigation { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Product? ProductIdNavigation { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Service? ServiceIdNavigation { get; set; }
}

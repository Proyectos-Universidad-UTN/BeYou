using BeYou.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Models;

[Table("PaymentType")]
public partial class PaymentType : BaseSimpleDto
{
    [StringLength(50)]
    public string Description { get; set; } = null!;

    [StringLength(50)]
    public string ReferenceNumber { get; set; } = null!;

    [InverseProperty("PaymentTypeIdNavigation")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("PaymentTypeIdNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class PaymentType : BaseSimpleDto
{
    public string Description { get; set; } = null!;

    public string ReferenceNumber { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

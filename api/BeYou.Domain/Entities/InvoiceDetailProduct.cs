using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class InvoiceDetailProduct : BaseSimpleDto
{
    public long InvoiceDetailId { get; set; }

    public long ProductId { get; set; }

    public decimal Quantity { get; set; }

    public virtual InvoiceDetail InvoiceDetail { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
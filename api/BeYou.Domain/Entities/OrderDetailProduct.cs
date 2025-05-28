using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class OrderDetailProduct : BaseSimpleDto
{
    public long OrderDetailId { get; set; }

    public long ProductId { get; set; }

    public decimal Quantity { get; set; }

    public virtual OrderDetail OrderDetail { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}

using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class OrderDetail : BaseSimpleDto
{
    public long OrderId { get; set; }

    public long? ServiceId { get; set; }

    public long? ProductId { get; set; }

    public byte LineNumber { get; set; }

    public short Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Tax { get; set; }

    public decimal Total { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<OrderDetailProduct> OrderDetailProducts { get; set; } = new List<OrderDetailProduct>();

    public virtual Product? Product { get; set; }

    public virtual Service? Service { get; set; }
}
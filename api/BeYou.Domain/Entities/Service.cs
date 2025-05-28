using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Service : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public long TypeServiceId { get; set; }

    public decimal Price { get; set; }

    public string? Observation { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    public virtual TypeService TypeService { get; set; } = null!;
}

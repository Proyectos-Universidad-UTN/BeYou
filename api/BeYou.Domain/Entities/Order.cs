using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Order : BaseEntity
{
    public long BranchId { get; set; }

    public long ReservationId { get; set; }

    public long CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public DateOnly Date { get; set; }

    public long PaymentTypeId { get; set; }

    public short Number { get; set; }

    public long TaxId { get; set; }

    public decimal TaxRate { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Tax { get; set; }

    public decimal Total { get; set; }

    public long StatusOrderId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual PaymentType PaymentType { get; set; } = null!;

    public virtual Reservation Reservation { get; set; } = null!;

    public virtual StatusOrder StatusOrder { get; set; } = null!;

    public virtual Tax TaxNavigation { get; set; } = null!;
}
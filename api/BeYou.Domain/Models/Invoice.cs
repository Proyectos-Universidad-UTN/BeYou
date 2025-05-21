using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class Invoice
{
    public long Id { get; set; }

    public long BranchId { get; set; }

    public long? OrderId { get; set; }

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

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual Order? Order { get; set; }

    public virtual PaymentType PaymentType { get; set; } = null!;

    public virtual Tax TaxNavigation { get; set; } = null!;
}

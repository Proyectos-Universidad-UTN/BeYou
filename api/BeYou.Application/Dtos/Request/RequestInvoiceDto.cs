namespace BeYou.Application.Dtos.Request;
public record RequestInvoiceDto : RequestBaseDto
{
    public long BranchId { get; set; }

    public long CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public long? OrderId { get; set; }

    public DateOnly Date { get; set; }

    public long PaymentTypeId { get; set; }

    public short Number { get; set; }

    public long TaxId { get; set; }

    public decimal TaxRate { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Tax { get; set; }

    public decimal Total { get; set; }

    public IEnumerable<RequestInvoiceDetailDto>? InvoiceDetails { get; set; }
}

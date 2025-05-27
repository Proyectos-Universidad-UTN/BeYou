namespace BeYou.Application.Dtos.Request;

public record RequestInvoiceDetailProductDto : RequestBaseDto
{
    public long InvoiceDetailId { get; set; }

    public long ProductId { get; set; }

    public decimal Quantity { get; set; }
}

using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseInvoiceDetailProductDto : BaseSimpleEntity
{
    public long InvoiceDetailId { get; set; }

    public long ProductId { get; set; }

    public decimal Quantity { get; set; }

    public virtual ResponseInvoiceDetailDto InvoiceDetail { get; set; } = null!;

    public virtual ResponseProductDto Product { get; set; } = null!;
}
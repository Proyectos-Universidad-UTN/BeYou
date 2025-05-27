using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseInvoiceDetailDto : BaseSimpleEntity
{
    public long InvoiceId { get; set; }

    public long? ServiceId { get; set; }

    public long? ProductId { get; set; }

    public byte LineNumber { get; set; }

    public short Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Tax { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<ResponseInvoiceDetailProductDto> InvoiceDetailProducts { get; set; } = new List<ResponseInvoiceDetailProductDto>();

    public virtual ResponseInvoiceDto Invoice { get; set; } = null!;

    public virtual ResponseServiceDto? Service { get; set; } = null!;
}

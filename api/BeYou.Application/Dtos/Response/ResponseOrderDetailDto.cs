using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseOrderDetailDto : BaseSimpleEntity
{
    public long OrderId { get; set; }

    public long? ServiceId { get; set; }

    public long? ProductId { get; set; }

    public byte LineNumber { get; set; }

    public short Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Tax { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<ResponseOrderDetailProductDto> OrderDetailProducts { get; set; } = new List<ResponseOrderDetailProductDto>();

    public virtual ResponseOrderDto Order { get; set; } = null!;

    public virtual ResponseServiceDto Service { get; set; } = null!;
}

using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseProductDto : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public long CategoryId { get; set; }

    public decimal Price { get; set; }

    public string Sku { get; set; } = null!;

    public long UnitMeasureId { get; set; }

    public virtual ICollection<ResponseInvoiceDetailProductDto> InvoiceDetailProducts { get; set; } = new List<ResponseInvoiceDetailProductDto>();

    public virtual ResponseCategoryDto Category { get; set; } = null!;

    public virtual ResponseUnitMeasureDto UnitMeasure { get; set; } = null!;

    public virtual ICollection<ResponseInventoryDto> Inventarios { get; set; } = new List<ResponseInventoryDto>();

    public virtual ICollection<ResponseInventoryProductDto> InventoryProducts { get; set; } = new List<ResponseInventoryProductDto>();
}

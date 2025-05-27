using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseInventoryProductDto : BaseEntity
{
    public long InventoryId { get; set; }

    public long ProductId { get; set; }

    public decimal Assignable { get; set; }

    public decimal Minimum { get; set; }

    public decimal Maximum { get; set; }

    public virtual ResponseInventoryDto Inventory { get; set; } = null!;

    public virtual ResponseProductDto Product { get; set; } = null!;
}

namespace BeYou.Application.Dtos.Request;

public record RequestProductDto : RequestBaseDto
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public long CategoryId { get; set; }

    public decimal Price { get; set; }

    public string Sku { get; set; } = null!;

    public long UnitMeasureId { get; set; }
}

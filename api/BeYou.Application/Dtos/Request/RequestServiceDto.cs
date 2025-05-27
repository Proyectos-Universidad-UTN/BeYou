namespace BeYou.Application.Dtos.Request;

public record RequestServiceDto : RequestBaseDto
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public long TypeServiceId { get; set; }

    public decimal Price { get; set; }

    public string? Observation { get; set; }
}

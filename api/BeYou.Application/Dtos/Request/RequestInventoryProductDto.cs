namespace BeYou.Application.Dtos.Request;

public record RequestInventoryProductDto : RequestBaseDto
{
    public long InventoryId { get; set; }

    public long ProductId { get; set; }

    public decimal Assignable { get; set; }

    public decimal Minimum { get; set; }

    public decimal Maximum { get; set; }
}


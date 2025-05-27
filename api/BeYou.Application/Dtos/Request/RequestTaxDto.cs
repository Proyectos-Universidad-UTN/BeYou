namespace BeYou.Application.Dtos.Request;

public record RequestTaxDto : RequestBaseDto
{
    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }
}

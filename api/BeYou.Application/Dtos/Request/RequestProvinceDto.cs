namespace BeYou.Application.Dtos.Request;

public record RequestProvinceDto : RequestBaseDto
{
    public string Name { get; set; } = null!;
}

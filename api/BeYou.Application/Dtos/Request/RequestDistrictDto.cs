namespace BeYou.Application.Dtos.Request;

public record RequestDistrictDto : RequestBaseDto
{
    public string Name { get; set; } = null!;

    public long CantonId { get; set; }
}
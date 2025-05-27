namespace BeYou.Application.Dtos.Request;

public record RequestGenderDto : RequestBaseDto
{
    public string Name { get; set; } = null!;
}

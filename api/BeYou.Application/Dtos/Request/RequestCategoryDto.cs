namespace BeYou.Application.Dtos.Request;

public record RequestCategoryDto : RequestBaseDto
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;
}
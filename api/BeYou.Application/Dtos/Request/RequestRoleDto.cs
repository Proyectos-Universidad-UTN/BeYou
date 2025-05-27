namespace BeYou.Application.Dtos.Request;

public record RequestRoleDto : RequestBaseDto
{
    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;
}
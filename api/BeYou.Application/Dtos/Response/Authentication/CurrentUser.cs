using BeYou.Application.Enums;

namespace BeYou.Application.Dtos.Response.Authentication;

public record CurrentUser
{
    public long UserId { get; init; }

    public string? Email { get; init; }  

    public string? FirstName { get; init; }  

    public string? LastName { get; init; } 

    public RoleApplication? Role { get; init; }

    public long? RoleId => (long?)Role;

    public string? RoleDescription => Role?.ToString();
}
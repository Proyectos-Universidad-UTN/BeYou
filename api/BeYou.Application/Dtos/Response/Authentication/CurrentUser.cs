using BeYou.Application.Enums;

namespace BeYou.Application.Dtos.Response.Authentication;

public record CurrentUser
{
    public long UserId { get; init; }

    public string? Email { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public RoleApplication? Role { get; init; }

    public long RoleId => (long)(Role ?? 0);

    public string RoleDescription => Role?.ToString() ?? string.Empty;
}

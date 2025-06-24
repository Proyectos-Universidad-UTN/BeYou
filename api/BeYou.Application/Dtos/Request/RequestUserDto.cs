namespace BeYou.Application.Dtos.Request;

public record RequestUserDto : RequestBaseDto
{
    public string CardId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int Telephone { get; set; }
    public string Email { get; set; } = null!;
    public long DistrictId { get; set; }
    public string? Address { get; set; }
    public DateOnly Birthday { get; set; }
    public string Password { get; set; } = null!;
    public long GenderId { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public long RoleId { get; set; }
}

namespace BeYou.Application.Dtos.Request;

public record RequestContactDto : RequestBaseDto
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public long VendorId { get; set; }
}
namespace BeYou.Application.Dtos.Request;

public record RequestCustomerDto : RequestBaseDto
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Telephone { get; set; }

    public long DistrictId { get; set; }

    public string? Address { get; set; }
}

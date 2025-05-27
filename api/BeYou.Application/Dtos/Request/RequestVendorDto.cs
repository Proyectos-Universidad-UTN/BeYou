namespace BeYou.Application.Dtos.Request;

public record RequestVendorDto : RequestBaseDto
{
    public string Name { get; set; } = null!;

    public string FiscalNumber { get; set; } = null!;

    public string SocialReason { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public long DistrictId { get; set; }

    public string? Address { get; set; }
}
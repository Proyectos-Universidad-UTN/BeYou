namespace BeYou.Application.Dtos.Request;

public record RequestPaymentTypeDto : RequestBaseDto
{
    public string Description { get; set; } = null!;

    public int ReferenceNumber { get; set; }
}

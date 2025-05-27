namespace BeYou.Application.Dtos.Request;

public record RequestOrderDetailProductDto : RequestBaseDto
{
    public long OrderDetailId { get; set; }

    public long ProductId { get; set; }

    public decimal Quantity { get; set; }
}

using BeYou.Application.Enums;

namespace BeYou.Application.Dtos.Request;

public record RequestInventoryProductTransactionDto : RequestBaseDto
{
    public long InventoryProductId { get; set; }

    public TransactionTypeInventoryApplication TransactionType { get; set; }

    public decimal Quantity { get; set; }
}
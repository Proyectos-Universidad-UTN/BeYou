using BeYou.Application.Enums;

namespace BeYou.Application.Dtos.Request;

public record RequestInventoryDto : RequestBaseDto
{
    public string Name { get; set; } = null!;

    public long BranchId { get; set; }

    public TypeInventoryApplication TypeInventory { get; set; }
}

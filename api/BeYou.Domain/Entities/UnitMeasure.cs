using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class UnitMeasure : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
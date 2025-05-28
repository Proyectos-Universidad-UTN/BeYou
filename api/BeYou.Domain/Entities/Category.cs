using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Category : BaseEntity
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
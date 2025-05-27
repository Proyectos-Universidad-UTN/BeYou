using BeYou.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Models;

[Table("Category")]
public partial class Category : BaseEntity
{
    [StringLength(50)]
    public string Code { get; set; } = null!;

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("CategoryIdNavigation")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
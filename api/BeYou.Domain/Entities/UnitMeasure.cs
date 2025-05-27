using BeYou.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Models;

[Table("UnitMeasure")]
public partial class UnitMeasure : BaseEntity
{
    [StringLength(25)]
    public string Name { get; set; } = null!;

    [StringLength(5)]
    public string Symbol { get; set; } = null!;

    [InverseProperty("UnitMeasureIdNavigation")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

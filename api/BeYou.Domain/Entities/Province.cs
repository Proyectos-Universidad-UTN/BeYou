using BeYou.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Models;

[Table("Province")]
public partial class Province : BaseSimpleDto
{
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("ProvinceIdNavigation")]
    public virtual ICollection<Canton> Cantons { get; set; } = new List<Canton>();
}
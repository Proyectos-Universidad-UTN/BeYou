using BeYou.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Models;

[Table("TypeService")]
public partial class TypeService : BaseSimpleDto
{
    [StringLength(50)]
    public string Name { get; set; } = null!;

    public TimeOnly BaseDuration { get; set; }

    [InverseProperty("TypeServiceIdNavigation")]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}

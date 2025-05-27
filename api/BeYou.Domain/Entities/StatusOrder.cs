using BeYou.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Models;

[Table("StatusOrder")]
public partial class StatusOrder : BaseSimpleDto
{
    [StringLength(25)]
    public string Description { get; set; } = null!;

    [InverseProperty("StatusOrderIdNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

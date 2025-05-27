using BeYou.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Models;

[Table("Role")]
public partial class Role : BaseEntity
{
    [StringLength(50)]
    public string Description { get; set; } = null!;

    [StringLength(50)]
    public string Type { get; set; } = null!;

    [InverseProperty("RoleIdNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
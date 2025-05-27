using BeYou.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Models;

[Table("Gender")]
public partial class Gender : BaseSimpleDto
{
    [StringLength(25)]
    public string Name { get; set; } = null!;

    [InverseProperty("GenderIdNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Role : BaseEntity
{
    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

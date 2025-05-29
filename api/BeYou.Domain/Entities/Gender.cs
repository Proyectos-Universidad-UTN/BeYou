using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Gender : BaseSimpleDto
{
    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
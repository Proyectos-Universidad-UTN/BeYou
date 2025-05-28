using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class TypeService : BaseSimpleDto
{
    public string Name { get; set; } = null!;

    public TimeOnly BaseDuration { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
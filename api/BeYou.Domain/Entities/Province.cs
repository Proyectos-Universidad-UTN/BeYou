using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Province : BaseSimpleDto
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Canton> Cantons { get; set; } = new List<Canton>();
}

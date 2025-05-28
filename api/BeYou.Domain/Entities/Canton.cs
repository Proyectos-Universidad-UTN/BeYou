using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;
public partial class Canton : BaseSimpleDto
{
    public string Name { get; set; } = null!;

    public long ProvinceId { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual Province Province { get; set; } = null!;
}
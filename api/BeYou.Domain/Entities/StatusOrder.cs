using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class StatusOrder : BaseSimpleDto
{
    public string Description { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
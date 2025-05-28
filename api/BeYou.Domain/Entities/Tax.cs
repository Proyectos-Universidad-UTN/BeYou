using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Tax : BaseEntity
{
    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
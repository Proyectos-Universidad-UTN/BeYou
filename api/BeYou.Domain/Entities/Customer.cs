using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Customer : BaseEntity
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Telephone { get; set; }

    public long DistrictId { get; set; }

    public string? Address { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
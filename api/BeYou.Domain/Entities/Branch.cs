using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Branch : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public long DistrictId { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<BranchHoliday> BranchHolidays { get; set; } = new List<BranchHoliday>();

    public virtual ICollection<BranchSchedule> BranchSchedules { get; set; } = new List<BranchSchedule>();

    public virtual District District { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
}
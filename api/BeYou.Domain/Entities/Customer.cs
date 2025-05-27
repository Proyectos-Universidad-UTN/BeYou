using BeYou.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Models;

[Table("Customer")]
[Index("DistrictId", Name = "IX_Customer_DistrictId")]
public partial class Customer : BaseEntity
{
    [StringLength(80)]
    public string FirstName { get; set; } = null!;

    [StringLength(80)]
    public string LastName { get; set; } = null!;

    [StringLength(150)]
    public string Email { get; set; } = null!;

    public int Telephone { get; set; }

    public long DistrictId { get; set; }

    [StringLength(250)]
    public string? Address { get; set; }

    [InverseProperty("CustomerIdNavigation")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [ForeignKey("DistrictId")]
    [InverseProperty("Customers")]
    public virtual District DistrictIdNavigation { get; set; } = null!;

    [InverseProperty("CustomerIdNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("CustomerIdNavigation")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
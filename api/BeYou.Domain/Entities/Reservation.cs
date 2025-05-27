using BeYou.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BeYou.Domain.Models;

[Table("Reservation")]
[Index("CustomerId", Name = "IX_Reservation_CustomerId")]
[Index("BranchId", Name = "IX_Reservation_BranchId")]
public partial class Reservation : BaseEntity
{
    public long BranchId { get; set; }

    public long CustomerId { get; set; }

    [StringLength(80)]
    public string CustomerName { get; set; } = null!;

    public DateOnly Date { get; set; }

    public TimeOnly Hour { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [InverseProperty("ReservationIdNavigation")]
    public virtual ICollection<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    [ForeignKey("CustomerId")]
    [InverseProperty("Reservations")]
    public virtual Customer CustomerIdNavigation { get; set; } = null!;

    [ForeignKey("BranchId")]
    [InverseProperty("Reservations")]
    public virtual Branch BranchIdNavigation { get; set; } = null!;

    [InverseProperty("ReservationIdNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("ReservationIdNavigation")]
    public virtual ICollection<ReservationQuestion> ReservationQuestions { get; set; } = new List<ReservationQuestion>();
}

using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Reservation : BaseEntity
{
    public long BranchId { get; set; }

    public long CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public DateOnly Date { get; set; }

    public TimeOnly Hour { get; set; }

    public string Status { get; set; } = null!;

    public virtual Branch Branch { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    public virtual ICollection<ReservationQuestion> ReservationQuestions { get; set; } = new List<ReservationQuestion>();
}
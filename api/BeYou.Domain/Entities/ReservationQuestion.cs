using BeYou.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Models;

[Table("ReservationQuestion")]
[Index("ReservationId", Name = "IX_ReservationQuestion_ReservationId")]
public partial class ReservationQuestion : BaseEntity
{
    public long ReservationId { get; set; }

    [StringLength(250)]
    public string Question { get; set; } = null!;

    [StringLength(250)]
    public string? Answer { get; set; }

    [ForeignKey("ReservationId")]
    [InverseProperty("ReservationQuestions")]
    public virtual Reservation ReservationIdNavigation { get; set; } = null!;
}
using BeYou.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Models;


[Table("TokenMaster")]
[Index("UserId", Name = "IX_TokenMaster_UserId")]
public partial class TokenMaster : BaseEntity
{
    [StringLength(250)]
    public string Token { get; set; } = null!;

    [StringLength(250)]
    public string JwtId { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ExpireAt { get; set; }

    public bool Used { get; set; }

    public long UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TokenMasters")]
    public virtual User UserIdNavigation { get; set; } = null!;
}
using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class TokenMaster : BaseEntity
{
    public string Token { get; set; } = null!;

    public string JwtId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpireAt { get; set; }

    public bool Used { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
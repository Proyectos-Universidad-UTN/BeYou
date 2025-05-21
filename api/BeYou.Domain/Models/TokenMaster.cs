using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class TokenMaster
{
    public long Id { get; set; }

    public string Token { get; set; } = null!;

    public string JwtId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpireAt { get; set; }

    public bool Used { get; set; }

    public long UserId { get; set; }

    public bool Active { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual User User { get; set; } = null!;
}

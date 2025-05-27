using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class UserBranch
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long BranchId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

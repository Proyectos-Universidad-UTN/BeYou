using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class UserBranch : BaseSimpleDto
{
    public long UserId { get; set; }

    public long BranchId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
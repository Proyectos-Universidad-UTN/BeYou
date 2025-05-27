using BeYou.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeYou.Domain.Models;

[Table("UserBranch")]
[Index("BranchId", Name = "IX_UserBranch_BranchId")]
[Index("UserId", Name = "IX_UserBranch_UserId")]
public partial class UserBranch : BaseSimpleDto
{
    public long UserId { get; set; }

    public long BranchId { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("UserBranches")]
    public virtual Branch BranchIdNavigation { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserBranches")]
    public virtual User UserIdNavigation { get; set; } = null!;
}
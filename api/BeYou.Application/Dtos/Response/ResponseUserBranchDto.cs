using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;
public record ResponseUserBranchDto : BaseSimpleEntity
{
    public long UserId { get; set; }

    public long BranchId { get; set; }

    public virtual ResponseBranchDto Branch { get; set; } = null!;

    public virtual ResponseUserDto User { get; set; } = null!;
}

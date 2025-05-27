namespace BeYou.Application.Dtos.Request;

public record RequestUserBranchDto : RequestBaseDto
{
    public long UserId { get; set; }

    public long BranchId { get; set; }
}

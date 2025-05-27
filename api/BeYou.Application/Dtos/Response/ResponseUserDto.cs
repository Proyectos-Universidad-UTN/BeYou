using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseUserDto : BaseEntity
{
    public string CardId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public long DistrictId { get; set; }

    public string? Address { get; set; }

    public DateOnly Birthday { get; set; }

    public string Password { get; set; } = null!;

    public long GenderId { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public long RoleId { get; set; }

    public virtual ResponseDistrictDto District { get; set; } = null!;

    public virtual ResponseGenderDto Gender { get; set; } = null!;

    public virtual ResponseRoleDto Role { get; set; } = null!;

    public virtual ICollection<ResponseUserBranchDto> UserBranches { get; set; } = new List<ResponseUserBranchDto>();
}

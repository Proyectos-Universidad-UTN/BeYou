using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class User : BaseEntity
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

    public virtual District District { get; set; } = null!;

    public virtual Gender Gender { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<TokenMaster> TokenMasters { get; set; } = new List<TokenMaster>();

    public virtual ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
}
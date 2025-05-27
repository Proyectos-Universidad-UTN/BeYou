using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class User
{
    public long Id { get; set; }

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

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual Gender Gender { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<TokenMaster> TokenMasters { get; set; } = new List<TokenMaster>();

    public virtual ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
}

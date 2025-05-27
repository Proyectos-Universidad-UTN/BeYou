using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class Role
{
    public long Id { get; set; }

    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

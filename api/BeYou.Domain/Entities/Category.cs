using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class Category
{
    public long Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class Canton
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long ProvinceId { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual Province Province { get; set; } = null!;
}

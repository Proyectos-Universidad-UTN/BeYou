using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class TypeService
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public TimeOnly BaseDuration { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}

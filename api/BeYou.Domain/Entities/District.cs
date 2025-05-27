using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class District
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long CantonId { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual Canton Canton { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}

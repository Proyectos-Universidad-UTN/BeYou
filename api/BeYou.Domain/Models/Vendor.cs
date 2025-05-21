using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class Vendor
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string FiscalNumber { get; set; } = null!;

    public string SocialReason { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public long DistrictId { get; set; }

    public string? Address { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual District District { get; set; } = null!;
}

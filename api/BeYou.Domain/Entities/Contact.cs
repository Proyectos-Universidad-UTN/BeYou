using System;
using System.Collections.Generic;

namespace BeYou.Domain.Models;

public partial class Contact
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public long VendorId { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual Vendor Vendor { get; set; } = null!;
}

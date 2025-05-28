using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Contact : BaseEntity
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public long VendorId { get; set; }

    public virtual Vendor Vendor { get; set; } = null!;
}
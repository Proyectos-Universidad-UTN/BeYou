using BeYou.Domain.Core.Models;

namespace BeYou.Domain.Models;

public partial class Vendor : BaseEntity
{
    public string Name { get; set; } = null!;

    public string FiscalNumber { get; set; } = null!;

    public string SocialReason { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public long DistrictId { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual District District { get; set; } = null!;
}
using BeYou.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Models;

[Table("Vendor")]
[Index("DistrictId", Name = "IX_Vendor_DistrictId")]
public partial class Vendor : BaseEntity
{
    [StringLength(80)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    public string FiscalNumber { get; set; } = null!;

    [StringLength(150)]
    public string SocialReason { get; set; } = null!;

    public int Telephone { get; set; }

    [StringLength(150)]
    public string Email { get; set; } = null!;

    public long DistrictId { get; set; }

    [StringLength(250)]
    public string? Address { get; set; }

    [InverseProperty("VendorIdNavigation")]
    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    [ForeignKey("DistrictId")]
    [InverseProperty("Vendors")]
    public virtual District DistrictIdNavigation { get; set; } = null!;
}
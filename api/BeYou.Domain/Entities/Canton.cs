using BeYou.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BeYou.Domain.Models;


[Table("Canton")]
[Index("ProvinceId", Name = "IX_Canton_ProvinceId")]
public partial class Canton : BaseSimpleDto
{
    [StringLength(50)]
    public string Name { get; set; } = null!;

    public long ProvinceId { get; set; }

    [InverseProperty("CantonIdNavigation")]
    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    [ForeignKey("ProvinceId")]
    [InverseProperty("Cantons")]
    public virtual Province ProvinceIdNavigation { get; set; } = null!;
}

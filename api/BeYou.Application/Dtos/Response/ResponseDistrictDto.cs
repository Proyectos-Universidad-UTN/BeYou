using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseDistrictDto : BaseSimpleEntity
{
    public string Name { get; set; } = null!;

    public long CantonId { get; set; }

    public virtual ICollection<ResponseCustomerDto> Customers { get; set; } = new List<ResponseCustomerDto>();

    public virtual ResponseCantonDto Canton { get; set; } = null!;

    public virtual ICollection<ResponseVendorDto> Vendors { get; set; } = new List<ResponseVendorDto>();

    public virtual ICollection<ResponseBranchDto> Branches { get; set; } = new List<ResponseBranchDto>();

    public virtual ICollection<ResponseUserDto> Users { get; set; } = new List<ResponseUserDto>();
}

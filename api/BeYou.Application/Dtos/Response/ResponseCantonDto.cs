using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseCantonDto : BaseSimpleEntity
{
    public string Name { get; set; } = null!;

    public long ProvinceId { get; set; }

    public virtual ICollection<ResponseDistrictDto> Districts { get; set; } = new List<ResponseDistrictDto>();

    public virtual ResponseProvinceDto Province { get; set; } = null!;
}
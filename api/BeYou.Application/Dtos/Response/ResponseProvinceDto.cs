using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseProvinceDto : BaseSimpleEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<ResponseCantonDto> Cantons { get; set; } = new List<ResponseCantonDto>();
}
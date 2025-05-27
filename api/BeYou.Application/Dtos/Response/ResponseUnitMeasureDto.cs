using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;
public record ResponseUnitMeasureDto : BaseSimpleEntity
{
    public string Name { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public virtual ICollection<ResponseProductDto> Products { get; set; } = new List<ResponseProductDto>();
}
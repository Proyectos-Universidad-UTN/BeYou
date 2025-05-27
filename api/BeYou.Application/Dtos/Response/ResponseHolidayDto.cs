using BeYou.Application.Dtos.Response.Base;
using BeYou.Application.Enums;

namespace BeYou.Application.Dtos.Response;

public record ResponseHolidayDto : BaseEntity
{
    public string Name { get; set; } = null!;

    public MonthApplication Month { get; set; }

    public byte Day { get; set; }

    public virtual ICollection<ResponseBranchHolidayDto> BranchHolidays { get; set; } = new List<ResponseBranchHolidayDto>();
}
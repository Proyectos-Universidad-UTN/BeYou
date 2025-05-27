
using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseBranchHolidayDto : BaseSimpleEntity
{
    public long HolidayId { get; set; }

    public long BranchId { get; set; }

    public DateOnly Date { get; set; }

    public short Year { get; set; }

    public virtual ResponseHolidayDto Holiday { get; set; } = null!;

    public virtual ResponseBranchDto Branch { get; set; } = null!;
}
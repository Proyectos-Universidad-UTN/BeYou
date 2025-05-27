using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseBranchDto : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public long DistrictId { get; set; }

    public string? Address { get; set; }

    public virtual ResponseDistrictDto? District { get; set; } = null!;

    public virtual ICollection<ResponseInventoryDto> Inventories { get; set; } = new List<ResponseInventoryDto>();

    public virtual ICollection<ResponseBranchScheduleDto> BranchSchedules { get; set; } = new List<ResponseBranchScheduleDto>();

    public virtual ICollection<ResponseUserBranchDto> UserBranches { get; set; } = new List<ResponseUserBranchDto>();

    public virtual ICollection<ResponseBranchHolidayDto> BranchHolidays { get; set; } = new List<ResponseBranchHolidayDto>();

    public virtual ICollection<ResponseReservationDto> Reservas { get; set; } = new List<ResponseReservationDto>();

    public virtual ICollection<ResponseOrderDto> Orders { get; set; } = new List<ResponseOrderDto>();

    public virtual ICollection<ResponseInvoiceDto> Invoices { get; set; } = new List<ResponseInvoiceDto>();
}
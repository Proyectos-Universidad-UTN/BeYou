using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseCustomerDto : BaseEntity
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Telephone { get; set; }

    public long DistrictId { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<ResponseInvoiceDto> Invoices { get; set; } = new List<ResponseInvoiceDto>();

    public virtual ICollection<ResponseReservationDto> Reservations { get; set; } = new List<ResponseReservationDto>();

    public virtual ResponseDistrictDto District { get; set; } = null!;
}

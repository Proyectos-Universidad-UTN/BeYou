using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseServiceDto : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public long TypeServiceId { get; set; }

    public decimal Price { get; set; }

    public string? Observation { get; set; }

    public virtual ICollection<ResponseInvoiceDetailDto> InvoiceDetails { get; set; } = new List<ResponseInvoiceDetailDto>();

    public virtual ResponseTypeServiceDto TypeService { get; set; } = null!;

    public virtual ICollection<ResponseReservationDetailDto> DetalleReservas { get; set; } = new List<ResponseReservationDetailDto>();
}

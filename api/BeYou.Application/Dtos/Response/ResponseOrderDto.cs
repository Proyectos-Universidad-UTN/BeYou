using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseOrderDto : BaseEntity
{
    public long CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public DateOnly Date { get; set; }

    public long PaymentTypeId { get; set; }

    public short Number { get; set; }

    public long TaxId { get; set; }

    public long ReservationId { get; set; }

    public decimal TaxRate { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Tax { get; set; }

    public decimal Total { get; set; }

    public char StatusOrderId { get; set; }

    public long BranchId { get; set; }

    public virtual ICollection<ResponseOrderDetailDto> OrderDetails { get; set; } = new List<ResponseOrderDetailDto>();

    public virtual ResponseCustomerDto Customer { get; set; } = null!;

    public virtual ResponseTaxDto TaxInfo { get; set; } = null!;

    public virtual ResponsePaymentTypeDto PaymentType { get; set; } = null!;

    public virtual ResponseReservationDto Reservation { get; set; } = null!;

    public virtual ResponseBranchDto Branch { get; set; } = null!;
}
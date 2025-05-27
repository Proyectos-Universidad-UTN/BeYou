using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseInvoiceDto : BaseEntity
{
    public long CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public long? OrderId { get; set; }

    public DateOnly Date { get; set; }

    public long PaymentTypeId { get; set; }

    public short Number { get; set; }

    public long TaxId { get; set; }

    public decimal TaxRate { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Tax { get; set; }

    public decimal Total { get; set; }

    public long BranchId { get; set; }

    public virtual ICollection<ResponseInvoiceDetailDto> InvoiceDetails { get; set; } = new List<ResponseInvoiceDetailDto>();

    public virtual ResponseCustomerDto Customer { get; set; } = null!;

    public virtual ResponseTaxDto TaxInfo { get; set; } = null!;

    public virtual ResponsePaymentTypeDto PaymentType { get; set; } = null!;

    public virtual ResponseOrderDto? Order { get; set; } = null!;

    public virtual ResponseBranchDto Branch { get; set; } = null!;
}

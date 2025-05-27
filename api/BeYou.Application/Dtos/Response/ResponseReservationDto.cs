using BeYou.Application.Dtos.Response.Base;

namespace BeYou.Application.Dtos.Response;

public record ResponseReservationDto : BaseEntity
{
    public DateOnly Date { get; set; }

    public TimeOnly Hour { get; set; }

    public long BranchId { get; set; }

    public long CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ResponseBranchDto Branch { get; set; } = null!;

    public virtual ResponseCustomerDto Customer { get; set; } = null!;

    public virtual ICollection<ResponseReservationQuestionDto> ReservationQuestions { get; set; } = new List<ResponseReservationQuestionDto>();

    public virtual ICollection<ResponseReservationDetailDto> ReservationDetails { get; set; } = new List<ResponseReservationDetailDto>();

    public virtual ICollection<ResponseOrderDto> Orders { get; set; } = new List<ResponseOrderDto>();
}

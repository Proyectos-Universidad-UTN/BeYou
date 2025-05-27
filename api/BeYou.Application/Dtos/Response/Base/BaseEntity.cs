namespace BeYou.Application.Dtos.Response.Base;

public record BaseEntity
{
    public long Id { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }
}

namespace BeYou.Domain.Core.Models;
public class BaseDto
{
    public long Id { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }
}
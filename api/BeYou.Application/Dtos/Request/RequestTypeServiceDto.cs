namespace BeYou.Application.Dtos.Request;
public record RequestTypeServiceDto : RequestBaseDto
{
    public string Name { get; set; } = null!;

    public TimeOnly BaseDuration { get; set; }
}

namespace BeYou.Application.Dtos.Request;
public record RequestCantonDto
{
    public string Name { get; set; } = null!;
    //*Comentario
    public long ProvinceId { get; set; }
}

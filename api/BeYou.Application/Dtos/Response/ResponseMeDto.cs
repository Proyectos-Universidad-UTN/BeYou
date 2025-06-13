namespace BeYou.Application.Dtos.Response;

public record ResponseMeDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public long RolId { get; set; }
    public string RolDescripcion { get; set; } = string.Empty;
}

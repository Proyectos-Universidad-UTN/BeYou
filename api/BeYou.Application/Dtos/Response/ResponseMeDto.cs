namespace BeYou.Application.Dtos.Response;

public record ResponseMeDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName { get { return $"{FirstName} {LastName}"; } }
    public ResponseRoleDto Role { get; set; } = null!;
}

using BeYou.Application.Dtos.Response;

public interface IServiceUserContext
{
    string? UserId { get; }
    ResponseMeDto? GetCurrentUser(); 
}

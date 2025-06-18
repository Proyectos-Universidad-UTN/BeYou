using BeYou.Application.Dtos.Response;

public interface IServiceUserContext
{
    string? UserId { get; }
    Task<ResponseMeDto> GetCurrentUserAsync(); 
}

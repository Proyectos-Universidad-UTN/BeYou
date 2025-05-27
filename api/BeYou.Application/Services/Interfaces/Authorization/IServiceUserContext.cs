namespace BeYou.Application.Services.Interfaces.Authorization;

public interface IServiceUserContext
{
    /// <summary>
    /// User email base on context sent from context
    /// </summary>
    /// <value>string</value>
    string? UserId { get; }
}
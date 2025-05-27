using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceReservationQuestion
{
    /// <summary>
    /// Get list of all reservations questions
    /// </summary>
    /// <returns>ICollection of ResponseReservationQuestionDto</returns>
    Task<ICollection<ResponseReservationQuestionDto>> ListAllAsync();

    /// <summary>
    /// Get reservation question with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseReservationQuestionDto</returns>
    Task<ResponseReservationQuestionDto> FindByIdAsync(long id);
}

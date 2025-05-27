using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceService
{
    /// <summary>
    /// Get list of all services
    /// </summary>
    /// <returns>ICollection of ResponseServiceDto</returns>
    Task<ICollection<ResponseServiceDto>> ListAllAsync();

    /// <summary>
    /// Get Service with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseServiceDto</returns>
    Task<ResponseServiceDto> FindByIdAsync(long id);

    /// <summary>
    /// Create a service
    /// </summary>
    /// <param name="serviceDto">Request servide model to be added</param>
    /// <returns>ResponseServiceDto</returns>
    Task<ResponseServiceDto> CreateServiceAsync(RequestServiceDto serviceDto);

    /// <summary>
    /// Update a service
    /// </summary>
    /// <param name="id">Id to identify record</param>
    /// <param name="serviceDto">Request service model to be updated</param>
    /// <returns>ResponseServiceDto</returns>
    Task<ResponseServiceDto> UpdateServiceAsync(long id, RequestServiceDto serviceDto);

    /// <summary>
    /// Deletes a service based on the provided Id.
    /// </summary>
    /// <param name="id">Id of the service to delete.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> DeleteServiceAsync(long id);
}

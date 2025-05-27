using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServicePaymentType
{
    /// <summary>
    /// Get list of all payment types
    /// </summary>
    /// <returns>ICollection of ResponsePaymentTypeDto</returns>
    Task<ICollection<ResponsePaymentTypeDto>> ListAllAsync();
}

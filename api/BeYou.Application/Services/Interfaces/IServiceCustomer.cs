using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceCustomer
{
    /// <summary>
    /// Get list of all existing customer.
    /// </summary>
    /// <returns>ICollection of ResponseCustomerDto.</returns>
    Task<ICollection<ResponseCustomerDto>> ListAllAsync();

    /// <summary>
    ///  Get exact customer according to id, if not, get null
    /// </summary>
    /// <param name="id">The ID of the customer.</param>
    /// <returns>ResponseCustomerDto</returns>
    Task<ResponseCustomerDto?> FindByIdAsync(long id);

    /// <summary>
    /// Deletes a customer based on the provided Id.
    /// </summary>
    /// <param name="id">Id of the customer to delete.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> DeleteCustomerAsync(long id);
}

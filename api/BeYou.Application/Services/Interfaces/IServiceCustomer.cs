using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceCustomer
{

    /// <summary>
    /// Create Customer
    /// </summary>
    /// <param name="customerDto">Customer request model to be added</param>
    /// <returns>ResponseCustomerDto</returns>
    Task<ResponseCustomerDto> CreateCustomerAsync(RequestCustomerDto customerDto);

    /// <summary>
    /// Get list of all existing customer.
    /// </summary>
    /// <returns>ICollection of ResponseCustomerDto.</returns>
    ///
    Task<ICollection<ResponseCustomerDto>> ListAllAsync();

    /// <summary>
    ///  Get exact customer according to id, if not, get null
    /// </summary>
    /// <param name="id">The ID of the customer.</param>
    /// <returns>ResponseCustomerDto</returns>
    Task<ResponseCustomerDto?> FindByIdAsync(long id);


    /// <summary>
    /// Update customer
    /// </summary>
    /// <param name="id">customer id</param>
    /// <param name="customerDto">customer request model to be updated</param>
    /// <returns>ResponseVendorDto</returns>
    Task<ResponseCustomerDto> UpdateCustomerAsync(long id, RequestCustomerDto customerDto);

    /// <summary>
    /// Deletes a customer based on the provided Id.
    /// </summary>
    /// <param name="id">Id of the customer to delete.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> DeleteCustomerAsync(long id);
}

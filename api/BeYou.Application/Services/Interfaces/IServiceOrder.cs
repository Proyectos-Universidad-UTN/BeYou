using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceOrder
{
    /// <summary>
    /// Create order
    /// </summary>
    /// <param name="orderDto">Order request model to be added</param>
    /// <returns>ResponseOrderDto</returns>
    Task<ResponseOrderDto> CreateOrderAsync(RequestOrderDto orderDto);

    /// <summary>
    /// Validate if order exists
    /// </summary>
    /// <param name="id">Order id</param>
    /// <returns>True if exists, false if not</returns>
    Task<bool> ExistsOrderAsync(long id);

    /// <summary>
    /// Get list of all orders
    /// </summary>
    /// <returns>ICollection of ResponseOrderDto</returns>
    Task<ICollection<ResponseOrderDto>> ListAllAsync();

    /// <summary>
    /// Get Order by Id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseOrderDto</returns>
    Task<ResponseOrderDto> FindByIdAsync(long id);
}

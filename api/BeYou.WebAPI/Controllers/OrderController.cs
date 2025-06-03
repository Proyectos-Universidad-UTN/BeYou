using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.WebAPI.Configuration;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Controller for managing orders (Orders).
/// </summary>
/// <param name="serviceOrder">The service used for order operations.</param>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class OrderController(IServiceOrder serviceOrder) : ControllerBase
{
    /// <summary>
    /// Retrieves all orders.
    /// </summary>
    /// <returns>A list of orders.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseOrderDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var orders = await serviceOrder.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, orders);
    }

    /// <summary>
    /// Retrieves an order by its ID.
    /// </summary>
    /// <param name="orderId">The ID of the order to retrieve.</param>
    /// <returns>The details of the specified order.</returns>
    [HttpGet("{orderId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseOrderDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(long orderId)
    {
        var order = await serviceOrder.FindByIdAsync(orderId);
        return StatusCode(StatusCodes.Status200OK, order);
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="order">The order data to be created.</param>
    /// <returns>The details of the created order.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseOrderDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateOrderAsync([FromBody] RequestOrderDto order)
    {
        ArgumentNullException.ThrowIfNull(order);
        var result = await serviceOrder.CreateOrderAsync(order);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}
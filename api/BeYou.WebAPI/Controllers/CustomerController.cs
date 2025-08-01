using Asp.Versioning;
using BeYou.Application.Dtos.Response;
using BeYou.WebAPI.Configuration;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeYou.Application.Dtos.Request;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the controller with the specified client service.
/// </summary>
/// <param name="serviceCustomer">The service used for client operations.</param>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class CustomerController(IServiceCustomer serviceCustomer) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all customers.
    /// </summary>
    /// <returns>A list of all clients.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseCustomerDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var customers = await serviceCustomer.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, customers);
    }

    /// <summary>
    /// Deletes a customer by its ID.
    /// </summary>
    /// <param name="customerId">The ID of the holiday to delete.</param>
    /// <returns>The deleted holiday.</returns>
    [HttpDelete("{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseCustomerDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> DeleteCustomerAsync(byte customerId)
    {
        var customer = await serviceCustomer.DeleteCustomerAsync(customerId);
        return StatusCode(StatusCodes.Status200OK, customer);
    }


    /// <summary>
    /// Creates a new customer.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseCustomerDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateCustomerAsync([FromBody] RequestCustomerDto customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        var result = await serviceCustomer.CreateCustomerAsync(customer);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Updates an existing customer by its ID.
    /// </summary>
    [HttpPut("{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseCustomerDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> UpdateCustomerAsync(short customerId, [FromBody] RequestCustomerDto customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        var result = await serviceCustomer.UpdateCustomerAsync(customerId, customer);
        return StatusCode(StatusCodes.Status200OK, result);
    }


    /// <summary>
    /// Retrieves a specific customer by its ID.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>The details of the specified customer.</returns>
    [HttpGet("{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseCustomerDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> GetCustomerByIdAsync(short customerId)
    {
        var customer = await serviceCustomer.FindByIdAsync(customerId);
        return StatusCode(StatusCodes.Status200OK, customer);
    }
}
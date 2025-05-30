using Asp.Versioning;
using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeYou.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the controller with the specified invoice service.
/// </summary>
/// <param name="serviceInvoice">The service used for invoice operations.</param>
[ApiController]
[BeYouAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BeYou")]
public class InvoiceController(IServiceInvoice serviceInvoice) : ControllerBase
{
    /// <summary>
    /// Retrieves all invoices.
    /// </summary>
    /// <returns>A list of all invoices.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseInvoiceDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> ListAllAsync()
    {
        var invoices = await serviceInvoice.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, invoices);
    }

    /// <summary>
    /// Retrieves a specific invoice by its ID.
    /// </summary>
    /// <param name="invoiceId">The ID of the invoice.</param>
    /// <returns>The details of the specified invoice.</returns>
    [HttpGet("{invoiceId}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseInvoiceDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> FindByIdAsync(long invoiceId)
    {
        var invoice = await serviceInvoice.FindByIdAsync(invoiceId);
        return StatusCode(StatusCodes.Status200OK, invoice);
    }

    /// <summary>
    /// Creates a new invoice.
    /// </summary>
    /// <param name="invoice">The invoice data to be created.</param>
    /// <returns>The created invoice.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseInvoiceDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBeYou))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBeYou))]
    public async Task<IActionResult> CreateInvoiceAsync([FromBody] RequestInvoiceDto invoice)
    {
        ArgumentNullException.ThrowIfNull(invoice);
        var result = await serviceInvoice.CreateInvoiceAsync(invoice);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}
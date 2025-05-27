using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;


namespace BeYou.Application.Services.Interfaces;

public interface IServiceInvoice
{
    /// <summary>
    /// Creates a new Invoice 
    /// </summary>
    /// <param name="invoiceDto">The data transfer object containing the information of the Invoice to create</param>
    /// <returns>RequestInvoiceDto</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseInvoiceDto> CreateInvoiceAsync(RequestInvoiceDto invoiceDto);

    /// <summary>
    /// Get list of all invoices
    /// </summary>
    /// <returns>ICollection of ResponseInvoiceDto</returns>
    Task<ICollection<ResponseInvoiceDto>> ListAllAsync();

    /// <summary>
    /// Finds a invoice by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the ResponseInvoiceDto to retrieve.</param>
    /// <returns>ResponseInvoiceDto</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseInvoiceDto> FindByIdAsync(long id);
}

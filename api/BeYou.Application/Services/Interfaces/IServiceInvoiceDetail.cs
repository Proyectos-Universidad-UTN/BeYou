using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceInvoiceDetail
{
    /// <summary>
    ///  Finds a invoice detail by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the invoice detail to retrieve.</param>
    /// <returns>ResponseInvoiceDetailDto</returns>
    Task<ResponseInvoiceDetailDto> FindByIdAsync(long id);

    /// <summary>
    /// Retrieves a list of all details by invoice.
    /// </summary>
    /// <param name="invoiceId">The identifier of the invoice</param>
    /// <returns>ICollection of ResponseInvoiceDetailDto</returns>
    Task<ICollection<ResponseInvoiceDetailDto>> ListAllByInvoiceAsync(long invoiceId);
}

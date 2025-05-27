using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceTax
{
    /// <summary>
    /// Retrieves a list of all taxes.
    /// </summary>
    /// <returns>
    /// An <see cref="ICollection{ResponseTaxDto}"/> containing all the tax details.
    /// </returns>
    Task<ICollection<ResponseTaxDto>> ListAllAsync();

    /// <summary>
    /// Finds a specific tax by its ID.
    /// </summary>
    /// <param name="id">The ID of the tax to be retrieved.</param>
    /// <returns>
    /// A <see cref="ResponseTaxDto"/> containing the details of the tax with the specified ID.
    /// </returns>
    Task<ResponseTaxDto> FindByIdAsync(long id);

    /// <summary>
    /// Creates a new tax entry.
    /// </summary>
    /// <param name="requestTaxDto">The tax data to be used for creating the new tax entry.</param>
    /// <returns>
    /// A <see cref="ResponseTaxDto"/> containing the details of the created tax entry.
    /// </returns>
    Task<ResponseTaxDto> CreateTaxAsync(RequestTaxDto requestTaxDto);

    /// <summary>
    /// Updates an existing tax entry.
    /// </summary>
    /// <param name="id">The ID of the tax to be retrieved.</param>
    /// <param name="requestTaxDto">The tax data to be used for updating the existing tax entry.</param>
    /// <returns>
    /// A <see cref="ResponseTaxDto"/> containing the updated details of the tax entry.
    /// </returns>
    Task<ResponseTaxDto> UpdateTaxAsync(long id, RequestTaxDto requestTaxDto);

    /// <summary>
    /// Deletes a tax entry by its ID.
    /// </summary>
    /// <param name="id">The ID of the tax to be deleted.</param>
    /// <returns>
    /// A boolean indicating whether the delete operation was successful.
    /// </returns>
    Task<bool> DeleteAsync(long id);

}
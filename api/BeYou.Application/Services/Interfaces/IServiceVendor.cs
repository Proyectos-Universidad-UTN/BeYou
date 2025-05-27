using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;
using BeYou.Application.Configuration.Pagination;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceVendor
{
    /// <summary>
    /// Get list of all vendors
    /// </summary>
    /// <returns>ICollection of ResponseVendorDto</returns>
    Task<ICollection<ResponseVendorDto>> ListAllAsync();

    /// <summary>
    /// Get list of all vendors paginated
    /// </summary>
    /// <param name="paginationParameters">Pagination paramets options</param>
    /// <returns>PagedList of ResponseVendorDto</returns>
    Task<PagedList<ResponseVendorDto>> ListAllAsync(PaginationParameters paginationParameters);

    /// <summary>
    /// Get vendor with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseVendorDto</returns>
    Task<ResponseVendorDto> FindByIdAsync(long id);

    /// <summary>
    /// Create vendor
    /// </summary>
    /// <param name="vendorDto">Vendor request model to be added</param>
    /// <returns>ResponseVendorDto</returns>
    Task<ResponseVendorDto> CreateVendorAsync(RequestVendorDto vendorDto);

    /// <summary>
    /// Update vendor
    /// </summary>
    /// <param name="id">Vendor id</param>
    /// <param name="vendorDto">Vendor request model to be updated</param>
    /// <returns>ResponseVendorDto</returns>
    Task<ResponseVendorDto> UpdateVendorAsync(long id, RequestVendorDto vendorDto);

    /// <summary>
    /// Delete existing vendor
    /// </summary>
    /// <param name="id">Vendor id</param>
    /// <returns>bool</returns>
    Task<bool> DeleteVendorAsync(long id);
}

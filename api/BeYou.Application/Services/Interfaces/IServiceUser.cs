using BeYou.Application.Dtos.Request;
using BeYou.Application.Dtos.Response;

namespace BeYou.Application.Services.Interfaces;

public interface IServiceUser
{
    /// <summary>
    /// Get list of all users
    /// </summary>
    /// <param name="role">Role name can be specified to filter</param>
    /// <returns>ICollection of ResponseUserDto</returns>
    Task<ICollection<ResponseUserDto>> ListAllAsync(string? role = null);

    /// <summary>
    /// Get user with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseUserDto</returns>
    Task<ResponseUserDto> FindByIdAsync(long id);

    /// <summary>
    /// Get user with specific email and password
    /// </summary>
    /// <param name="email">Email to look for</param>
    /// <param name="password">Password to look for</param>
    /// <returns>ResponseUserDto</returns>
    Task<ResponseUserDto> LoginAsync(string email, string password);

    /// <summary>
    /// Validate if the user can be assigned to another branch
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if exists, false if not</returns>
    Task<bool> ExistsUserAsync(long id);

    /// <summary>
    /// Get user with specific email
    /// </summary>
    /// <param name="email">Email to look for</param>
    /// <returns>ResponseUserDto</returns>
    Task<ResponseUserDto> FindByEmailAsync(string email);

    /// <summary>
    /// Create usuer
    /// </summary>
    /// <param name="userDTO">User request model to be added</param>
    /// <returns>ResponseUserDto</returns>
    Task<ResponseUserDto> CreateUserAsync(RequestUserDto userDTO);

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="id">user if</param>
    /// <param name="userDTO">User request model to be updated</param>
    /// <returns>ResponseUserDto</returns>
    Task<ResponseUserDto> UpdateUserAsync(long id, RequestUserDto UserDTO);
}
using BeYou.Application.Dtos.Response;
using BeYou.Application.Dtos.Response.Authentication;
using BeYou.Application.Services.Interfaces;
using BeYou.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace BeYou.Application.Services.Implementations.Authorization;

public class ServiceUserContext(IHttpContextAccessor httpContextAccessor, IServiceUser serviceUser) : IServiceUserContext
{
    public string? UserId
    {
        get
        {
            string? result = null;
            var httpContextItems = httpContextAccessor.HttpContext?.Items;
            if (httpContextItems != null && httpContextItems["CurrentUser"] is CurrentUser currentUser)
            {
                result = currentUser.Email;
            }

            if (string.IsNullOrEmpty(result))
            {
                result = Assembly.GetEntryAssembly()?.GetName().Name;
            }

            return result;
        }
    }

    public async Task<ResponseMeDto> GetCurrentUserAsync()
    {
        var httpContextItems = httpContextAccessor.HttpContext?.Items;
        if (httpContextItems != null && httpContextItems["CurrentUser"] is CurrentUser currentUser)
        {
            var user = await serviceUser.FindByIdAsync(currentUser.UserId);
            return new ResponseMeDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = new ResponseRoleDto
                {
                    Id = user.RoleId,
                    Description = user.Role.Description,
                    Type = user.Role.Type
                }
            };
        }

        throw new UnAuthorizedException("User not found in context or unauthorized access.");
    }
}
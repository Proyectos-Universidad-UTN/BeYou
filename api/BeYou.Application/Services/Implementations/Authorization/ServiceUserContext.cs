using BeYou.Application.Dtos.Response;
using BeYou.Application.Dtos.Response.Authentication;
using BeYou.Application.Services.Interfaces.Authorization;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace BeYou.Application.Services.Implementations.Authorization;

public class ServiceUserContext(IHttpContextAccessor httpContextAccessor) : IServiceUserContext
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

    public ResponseMeDto GetCurrentUser()
    {
        var httpContextItems = httpContextAccessor.HttpContext?.Items;
        if (httpContextItems != null && httpContextItems["CurrentUser"] is CurrentUser currentUser)
        {
            return new ResponseMeDto
            {
                Nombre = currentUser.FirstName,
                Apellido = currentUser.LastName,
                NombreCompleto = $"{currentUser.FirstName} {currentUser.LastName}",
                RolId = currentUser.RoleId,
                RolDescripcion = currentUser.RoleDescription
            };
        }

        return new ResponseMeDto(); // vacío si no está presente
    }
}

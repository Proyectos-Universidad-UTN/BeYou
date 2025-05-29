using Microsoft.Extensions.Logging;
using System.Net;

namespace BeYou.Domain.Exceptions;

public class BadRequestException : BaseException
{
    public override LogLevel LogLevel { get; set; } = LogLevel.Information;

    public override HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.NotFound;

    public BadRequestException(string mensaje) : base(mensaje)
    {
    }
}
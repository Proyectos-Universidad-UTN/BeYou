using Microsoft.Extensions.Logging;
using System.Net;
using System.Runtime.Serialization;

namespace BeYou.Domain.Exceptions;

public abstract class BaseException : Exception
{
    //Esta propiedad obliga a las clases hijas a especificar un nivel de severidad del log (por ejemplo: Info, Warning, Error)
    public abstract LogLevel LogLevel { get; set; }

    //Esta propiedad también es abstracta, y las clases derivadas deben definir un código de estado HTTP apropiado
    //(por ejemplo: 400 BadRequest, 404 NotFound, 500 InternalServerError).
    public abstract HttpStatusCode HttpStatusCode { get; set; }

    protected BaseException(string mensaje) : base(mensaje)
    {
    }

#pragma warning disable SYSLIB0051 // Type or member is obsolete
    protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
#pragma warning restore SYSLIB0051 // Type or member is obsolete
}
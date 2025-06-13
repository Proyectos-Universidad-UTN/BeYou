using Microsoft.Extensions.Logging;
using System.Net;
using System.Runtime.Serialization;

namespace BeYou.Domain.Exceptions;

[Serializable]
//Indica que esta clase puede ser serializada, es decir, convertida a un formato que pueda ser almacenado o
//transmitido (por ejemplo, a través de la red o en un archivo de log).
public class BeYouException : BaseException
{
    public override LogLevel LogLevel { get; set; } = LogLevel.Information;

    public override HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.Conflict;

    public BeYouException(string mensaje) : base(mensaje)
    {
    }

    protected BeYouException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
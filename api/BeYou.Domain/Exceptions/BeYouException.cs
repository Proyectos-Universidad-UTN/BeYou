using Microsoft.Extensions.Logging;
using System.Net;
using System.Runtime.Serialization;

namespace BeYou.Domain.Exceptions;

[Serializable]
// Indicates that this class can be serialized, meaning it can be converted into a format that can be stored or
// transmitted (for example, over the network or in a log file).
public class BeYouException : BaseException
{
    public override LogLevel LogLevel { get; set; } = LogLevel.Information;

    public override HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.Conflict;

    public BeYouException(string mensaje) : base(mensaje)
    {
    }

    protected BeYouException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
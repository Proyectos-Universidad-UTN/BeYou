using Microsoft.Extensions.Logging;
using System.Net;
using System.Runtime.Serialization;

namespace BeYou.Domain.Exceptions;

[Serializable]
public class ValidationEntityException : BaseException
{
    public override LogLevel LogLevel { get; set; }

    public override HttpStatusCode HttpStatusCode { get; set; }

    public ValidationEntityException(string mensaje) : base(mensaje)
    {
    }

    protected ValidationEntityException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
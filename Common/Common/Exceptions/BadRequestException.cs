using System.Net;

namespace Kvpbldsck.NastolochkiAPI.Common.Contract.Exceptions;

public sealed class BadRequestException : BaseHttpException
{
    public BadRequestException(string message)
        : base(message, HttpStatusCode.BadRequest)
    {
    }

    public BadRequestException(string message, Exception innerException)
        : base(message, innerException, HttpStatusCode.BadRequest)
    {
    }
}

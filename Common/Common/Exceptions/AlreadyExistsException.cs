using System.Net;

namespace Kvpbldsck.NastolochkiAPI.Common.Contract.Exceptions;

public sealed class AlreadyExistsException : BaseHttpException
{
    public AlreadyExistsException(string message)
        : base(message, HttpStatusCode.Conflict)
    {
    }

    public AlreadyExistsException(string message, Exception innerException)
        : base(message, innerException, HttpStatusCode.Conflict)
    {
    }
}

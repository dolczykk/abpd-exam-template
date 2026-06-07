using System.Net;

namespace Apbd.Shared.Exceptions;

public abstract class ApplicationBaseException(string message) : Exception(message)
{
    public abstract HttpStatusCode StatusCode { get; }
}
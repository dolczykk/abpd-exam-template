using System.Net;

namespace Apbd.Shared.Exceptions;

public class RequestHandlerNotFoundException(string message = "Request handler was not found.")
    : ApplicationBaseException(message)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
}

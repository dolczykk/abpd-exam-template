using System.Net;
using Apbd.Shared.Validation;

namespace Apbd.Shared.Exceptions;

public class RequestValidationException(IReadOnlyCollection<ValidationError> errors)
    : ApplicationBaseException("Request validation failed.")
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public IReadOnlyCollection<ValidationError> Errors { get; } = errors;
}

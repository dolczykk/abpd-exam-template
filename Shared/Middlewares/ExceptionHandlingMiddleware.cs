using Apbd.Shared.Exceptions;

using Microsoft.AspNetCore.Mvc;

namespace Apbd.Shared.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ApplicationBaseException exception)
        {
            await HandleApplicationException(context, exception);
        }
    }

    private static async Task HandleApplicationException(HttpContext context, ApplicationBaseException exception)
    {
        context.Response.StatusCode = (int)exception.StatusCode;
        context.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Status = (int)exception.StatusCode,
            Title = exception.Message
        };

        if (exception is RequestValidationException validationException)
        {
            problemDetails.Extensions["errors"] = validationException.Errors;
        }

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
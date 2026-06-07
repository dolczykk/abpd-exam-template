using Apbd.Shared.Exceptions;
using Apbd.Shared.Validation;

namespace Apbd.Shared.Cqrs;

public class Dispatcher(IServiceProvider provider) : IDispatcher
{
    public async Task<TResponse> Dispatch<TResponse>(IRequest<TResponse> request)
    {
        await ValidateRequest(request);

        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic? handler = provider.GetService(handlerType);

        if (handler is null)
        {
            throw new RequestHandlerNotFoundException();
        }

        return await handler.HandleAsync((dynamic)request);
    }

    private async Task ValidateRequest<TResponse>(IRequest<TResponse> request)
    {
        var validatorType = typeof(IRequestValidator<>).MakeGenericType(request.GetType());
        var validators = provider.GetServices(validatorType).ToArray();

        if (validators.Length == 0)
        {
            return;
        }

        var errors = new List<ValidationError>();

        foreach (var validator in validators.OfType<IRequestValidator>())
        {
            var validationErrors = await validator.Validate(request);
            errors.AddRange(validationErrors);
        }

        if (errors.Count > 0)
        {
            throw new RequestValidationException(errors);
        }
    }
}
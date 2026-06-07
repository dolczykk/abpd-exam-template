namespace Apbd.Shared.Validation;

public interface IRequestValidator
{
    Task<IReadOnlyCollection<ValidationError>> Validate(object request);
}

public interface IRequestValidator<in TRequest> : IRequestValidator
{
    Task<IReadOnlyCollection<ValidationError>> Validate(TRequest request);

    async Task<IReadOnlyCollection<ValidationError>> IRequestValidator.Validate(object request)
    {
        return await Validate((TRequest)request);
    }
}
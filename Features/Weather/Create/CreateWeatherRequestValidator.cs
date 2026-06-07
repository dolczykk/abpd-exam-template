using Apbd.Shared.Validation;

namespace Apbd.Features.Weather.Create;

public class CreateWeatherRequestValidator : IRequestValidator<CreateWeatherRequest>
{
    public Task<IReadOnlyCollection<ValidationError>> Validate(CreateWeatherRequest request)
    {
        var errors = new List<ValidationError>();

        if (string.IsNullOrWhiteSpace(request.City))
        {
            errors.Add(new ValidationError(nameof(request.City), "City is required."));
        }

        if (request.Temperature is < -100 or > 100)
        {
            errors.Add(new ValidationError(nameof(request.Temperature), "Temperature must be between -100 and 100."));
        }

        return Task.FromResult<IReadOnlyCollection<ValidationError>>(errors);
    }
}
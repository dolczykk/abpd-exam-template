using Apbd.Features.Weather.Dtos;
using Apbd.Shared.Cqrs;

namespace Apbd.Features.Weather.Create;

public record CreateWeatherRequest(string City, double Temperature) : IRequest<WeatherDto>;
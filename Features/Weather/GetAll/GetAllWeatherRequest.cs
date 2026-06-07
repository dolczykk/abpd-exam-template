using Apbd.Features.Weather.Dtos;
using Apbd.Shared.Cqrs;

namespace Apbd.Features.Weather.GetAll;

public record GetAllWeatherRequest : IRequest<WeatherDto[]>;
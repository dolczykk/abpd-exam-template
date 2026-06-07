using Apbd.Shared.Dtos;

namespace Apbd.Features.Weather.Dtos;

public record WeatherDto(Guid Id, string City, double Temperature) : IFrom<WeatherDto, Dal.Entities.Weather>
{
    public static WeatherDto From(Dal.Entities.Weather from)
        => new(from.Id, from.City, from.Temperature);
}
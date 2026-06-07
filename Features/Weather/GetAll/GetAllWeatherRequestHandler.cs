using Apbd.Dal;
using Apbd.Features.Weather.Dtos;
using Apbd.Shared.Cqrs;

namespace Apbd.Features.Weather.GetAll;

public class GetAllWeatherRequestHandler(IGenericRepository<Dal.Entities.Weather> weatherRepository) : IRequestHandler<GetAllWeatherRequest, WeatherDto[]>
{
    public async Task<WeatherDto[]> HandleAsync(GetAllWeatherRequest request)
    {
        var weathers = await weatherRepository.GetAll();

        return weathers.Select(WeatherDto.From).ToArray();
    }
}
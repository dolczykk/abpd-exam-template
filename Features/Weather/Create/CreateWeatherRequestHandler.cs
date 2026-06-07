using Apbd.Dal;
using Apbd.Features.Weather.Dtos;
using Apbd.Shared.Cqrs;

namespace Apbd.Features.Weather.Create;

public class CreateWeatherRequestHandler(
    IGenericRepository<Dal.Entities.Weather> weatherRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateWeatherRequest, WeatherDto>
{
    public async Task<WeatherDto> HandleAsync(CreateWeatherRequest request)
    {
        var weather = new Dal.Entities.Weather
        {
            City = request.City.Trim(),
            Temperature = request.Temperature
        };

        await weatherRepository.Add(weather);
        await unitOfWork.SaveChanges();

        return WeatherDto.From(weather);
    }
}
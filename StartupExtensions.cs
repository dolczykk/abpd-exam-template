using Apbd.Dal;
using Apbd.Dal.Entities;
using Apbd.Features.Weather.Create;
using Apbd.Features.Weather.Dtos;
using Apbd.Features.Weather.GetAll;
using Apbd.Shared.Cqrs;
using Apbd.Shared.Validation;

namespace Apbd;

public static class StartupExtensions
{
    extension(IServiceCollection services)
    {
        public void RegisterRequestHandlers()
        {
            services.AddScoped<IRequestHandler<CreateWeatherRequest, WeatherDto>, CreateWeatherRequestHandler>();
            services.AddScoped<IRequestHandler<GetAllWeatherRequest, WeatherDto[]>, GetAllWeatherRequestHandler>();
        }

        public void RegisterRequestValidators()
        {
            services.AddScoped<IRequestValidator<CreateWeatherRequest>, CreateWeatherRequestValidator>();
        }

        public void RegisterGenericRepositories()
        {
            services.AddScoped<IGenericRepository<Weather>, GenericRepository<Weather>>();
        }
    }
}
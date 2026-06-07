using Apbd.Dal;
using Apbd.Dal.Entities;
using Apbd.Features.Weather.Create;
using Apbd.Features.Weather.Dtos;
using Apbd.Features.Weather.GetAll;
using Apbd.Shared.Cqrs;
using Apbd.Shared.Middlewares;
using Apbd.Shared.Validation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDispatcher, Dispatcher>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

RegisterRequestHandlers(builder.Services);
RegisterRequestValidators(builder.Services);
RegisterGenericRepositories(builder.Services);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void RegisterRequestHandlers(IServiceCollection services)
{
    services.AddScoped<IRequestHandler<CreateWeatherRequest, WeatherDto>, CreateWeatherRequestHandler>();
    services.AddScoped<IRequestHandler<GetAllWeatherRequest, WeatherDto[]>, GetAllWeatherRequestHandler>();
}

void RegisterRequestValidators(IServiceCollection services)
{
    var validatorInterfaceType = typeof(IRequestValidator<>);
    var validators = typeof(Program).Assembly
        .GetTypes()
        .Where(type => type is { IsAbstract: false, IsInterface: false })
        .SelectMany(type => type.GetInterfaces()
            .Where(interfaceType => interfaceType.IsGenericType &&
                                    interfaceType.GetGenericTypeDefinition() == validatorInterfaceType)
            .Select(interfaceType => new
            {
                ServiceType = interfaceType,
                ImplementationType = type
            }));

    foreach (var validator in validators)
    {
        services.AddScoped(validator.ServiceType, validator.ImplementationType);
    }
}

void RegisterGenericRepositories(IServiceCollection services)
{
    services.AddScoped<IGenericRepository<Weather>, GenericRepository<Weather>>();
}

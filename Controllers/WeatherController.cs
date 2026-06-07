using Apbd.Features.Weather.Create;
using Apbd.Features.Weather.Dtos;
using Apbd.Features.Weather.GetAll;
using Apbd.Shared.Cqrs;

using Microsoft.AspNetCore.Mvc;

namespace Apbd.Controllers;

public class WeatherController(IDispatcher dispatcher) : BaseApiController(dispatcher)
{
    [HttpPost]
    [ProducesResponseType<WeatherDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateWeatherRequest request)
    {
        var result = await _dispatcher.Dispatch(request);

        return Created("/api/weather", result);
    }

    [HttpGet("all")]
    [ProducesResponseType<WeatherDto[]>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var request = new GetAllWeatherRequest();
        var result = await _dispatcher.Dispatch(request);

        return Ok(result);
    }
}
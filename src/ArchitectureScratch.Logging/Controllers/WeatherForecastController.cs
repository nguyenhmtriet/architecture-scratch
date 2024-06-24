using Logging.Models;
using Logging.Services;
using Microsoft.AspNetCore.Mvc;

namespace Logging.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class WeatherForecastController(AlwaysLogAsMessageTemplate service) : Controller
{
    private readonly AlwaysLogAsMessageTemplate _service = service;
    
    [HttpGet]
    [ProducesResponseType(typeof(WeatherForecast[]), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var result = _service.GetWeatherForecast();
        return Ok(result);
    }
}
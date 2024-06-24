using ArchitectureScratch.NSwagHttpClients.ApiClients;
using ArchitectureScratch.NSwagHttpClients.ApiClients.LoggingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureScratch.NSwagHttpClients.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestHttpClientController(ILoggingApiClientFacade loggingApiClientFacade) : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(WeatherForecastResult[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var result = await loggingApiClientFacade.GetWeatherForecasts(ct);
        return Ok(result);
    }
}
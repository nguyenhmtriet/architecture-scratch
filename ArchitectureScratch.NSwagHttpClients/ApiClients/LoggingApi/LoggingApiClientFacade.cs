using ArchitectureScratch.NSwagHttpClients.ApiClients.LoggingApi.Generated;
using ArchitectureScratch.NSwagHttpClients.ApiClients.LoggingApi.Models;
using DateOnly = System.DateOnly;

namespace ArchitectureScratch.NSwagHttpClients.ApiClients.LoggingApi;

internal class LoggingApiClientFacade(LoggingApiClient loggingApiClient) : ILoggingApiClientFacade
{
    public async Task<IReadOnlyCollection<WeatherForecastResult>> GetWeatherForecasts(CancellationToken ct)
    {
        var response = await loggingApiClient.WeatherForecastAsync(ct);
        return response.Select(x => new WeatherForecastResult
        {
            Date = new DateOnly(x.Date.Year, x.Date.Month, x.Date.Day),
            TemperatureC = x.TemperatureC,
            TemperatureF = x.TemperatureF,
            Summary = x.Summary
        }).ToList().AsReadOnly();
    }
}
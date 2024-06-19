using ArchitectureScratch.NSwagHttpClients.ApiClients.LoggingApi.Models;

namespace ArchitectureScratch.NSwagHttpClients.ApiClients;

public interface ILoggingApiClientFacade
{
    Task<IReadOnlyCollection<WeatherForecastResult>> GetWeatherForecasts(CancellationToken ct);
}
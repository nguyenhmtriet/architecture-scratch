using ArchitectureScratch.HttpClients.ApiClients.LoggingApi.Models;

namespace ArchitectureScratch.HttpClients.ApiClients;

public interface ILoggingApiClientFacade
{
    Task<IReadOnlyCollection<WeatherForecastResult>> GetWeatherForecasts(CancellationToken ct);
}
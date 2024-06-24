namespace ArchitectureScratch.NSwagHttpClients.ApiClients.LoggingApi.Models;

public record WeatherForecastResult
{
    public DateOnly Date { get; init; }
    public int TemperatureC { get; init; }
    public int TemperatureF { get; init; }
    public string? Summary { get; init; }
}
using System.Text.Json;
using Logging.Models;

namespace Logging.Services;

public class AlwaysLogAsMessageTemplate(ILogger<AlwaysLogAsMessageTemplate> logger)
{
    private readonly IList<string> Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public IEnumerable<WeatherForecast> GetWeatherForecast()
    {
        logger.LogInformation("Getting weather forecast at '{Time}'", DateTime.Now.ToString("U"));
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Count)]
                ))
            .ToArray();
        
        logger.LogInformation("Returning weather forecast at '{Time}' {ForecastJson}", DateTime.Now.ToString("U"), JsonSerializer.Serialize(forecast));
        return forecast;
    }
}
using ArchitectureScratch.NSwagHttpClients.ApiClients;
using ArchitectureScratch.NSwagHttpClients.ApiClients.LoggingApi;
using ArchitectureScratch.NSwagHttpClients.ApiClients.LoggingApi.Generated;
using ArchitectureScratch.NSwagHttpClients.Configuration;
using Microsoft.Extensions.Options;

namespace ArchitectureScratch.NSwagHttpClients.Extensions;

public static class HttpClientExtensions
{
    public static IServiceCollection ConfigureHttpClientApis(this IServiceCollection services)
    {
        services.AddScoped<ILoggingApiClientFacade, LoggingApiClientFacade>();
        services.AddHttpClient<LoggingApiClient>(
            (provider, client) =>
            {
                using var scope = provider.CreateScope();
                var config = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<HttpClientsConfiguration>>();
                client.BaseAddress = new Uri(config.Value.LoggingApi.BaseUrl);
            });
        
        return services;
    }
}
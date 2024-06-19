using Microsoft.ApplicationInsights.Extensibility;
using Serilog;

namespace Logging.Extensions;

public static class SerilogExtensions
{
    public static IServiceCollection ConfigureSerilog(this IServiceCollection services)
    {
        services.AddSerilog((provider, logger) =>
        {
            logger.ReadFrom.Configuration(provider.GetRequiredService<IConfiguration>());

            var telemetryConfig = provider.GetService<TelemetryConfiguration>();
            if (telemetryConfig == null) return;

            /*
             * this line will hit
             * if you're using Azure cloud and having Application Insights
             * just put the APPINSIGHTS_INSTRUMENTATIONKEY environment variable value
             * in appsettings.Production.json
             */
            logger.WriteTo.ApplicationInsights(telemetryConfig, TelemetryConverter.Traces);
        });

        return services;
    }
    
    public static IHostBuilder ConfigureSerilog(this IHostBuilder builder)
    {
        builder.UseSerilog((context, provider, logger) =>
        {
            logger.ReadFrom.Configuration(context.Configuration);
                
            var telemetryConfig = provider.GetService<TelemetryConfiguration>();
            if (telemetryConfig == null) return;
                
            logger.WriteTo.ApplicationInsights(telemetryConfig, TelemetryConverter.Traces);
        });

        return builder;
    }
}
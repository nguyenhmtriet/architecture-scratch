using ArchitectureScratch.MultipleAuthenticationSchemes.Configuration;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Extensions;

public static class AppSettingsOptionExtensions
{
    public static IServiceCollection AddAppSettingsOption(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<AppSettings>()
            .Bind(configuration.GetSection("App"))
            .ValidateOnStart()
            .ValidateDataAnnotations();

        return services;
    }
}

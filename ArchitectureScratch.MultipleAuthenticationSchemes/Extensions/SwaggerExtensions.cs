using ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Swagger;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        if (!configuration.IsSwaggerEnabled()) return services;

        services.AddSwaggerGen();
        services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, SwaggerGenOptionsConfigure>();
        services.AddSingleton<IConfigureOptions<SwaggerUIOptions>, SwaggerUiOptionsConfigure>();
        services.AddSingleton<IConfigureOptions<SwaggerOptions>, SwaggerOptionsConfigure>();

        return services;
    }

    public static bool IsSwaggerEnabled(this IConfiguration configuration)
    {
        bool.TryParse(configuration.GetSection("Swagger:Enabled").Value, out var isSwaggerEnabled);
        return isSwaggerEnabled;
    }
}

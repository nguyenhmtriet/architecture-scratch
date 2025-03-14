using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Swagger;

public class SwaggerOptionsConfigure(IConfiguration configuration) : IConfigureOptions<SwaggerOptions>
{
    public void Configure(SwaggerOptions options)
    {
        if (!string.IsNullOrEmpty(GetServerUrl(configuration)))
        {
            options.PreSerializeFilters.Add((swagger, httpReq) =>
            {
                swagger.Servers = new List<OpenApiServer> { new() { Url = GetServerUrl(configuration) } };
            });
        }
    }

    private static string GetServerUrl(IConfiguration configuration) =>
        configuration.GetSection("Swagger:ServerUrl").Value ??
        throw new NullReferenceException("Swagger:ServerUrl is not configured in settings json");
}

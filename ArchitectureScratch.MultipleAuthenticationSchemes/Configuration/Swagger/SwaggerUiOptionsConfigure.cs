using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Swagger;

public class SwaggerUiOptionsConfigure : IConfigureOptions<SwaggerUIOptions>
{
    public void Configure(SwaggerUIOptions options)
    {
        options.DisplayRequestDuration();
        options.ShowCommonExtensions();

        options.SwaggerEndpoint($"{SwaggerAreas.UserWorkspace}/swagger.json", SwaggerAreas.UserWorkspace);
        options.SwaggerEndpoint($"{SwaggerAreas.CustomerWorkspace}/swagger.json", SwaggerAreas.CustomerWorkspace);
    }
}

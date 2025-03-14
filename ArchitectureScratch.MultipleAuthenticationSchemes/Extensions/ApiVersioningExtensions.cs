using Asp.Versioning;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Extensions;

public static class ApiVersioningExtensions
{
    public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddApiVersioning(
                setup =>
                {
                    // must route to specific apiVersion
                    setup.AssumeDefaultVersionWhenUnspecified = true;
                    setup.DefaultApiVersion = new ApiVersion(1, 0);
                    setup.ReportApiVersions = true;

                    /*
                     * Read api-version from headers
                     * Headers["Api-Version"] = 1.0
                     * Headers["Accept"] = application/json;version=1.0
                     */
                    setup.ApiVersionReader = ApiVersionReader.Combine(
                        new UrlSegmentApiVersionReader()
                    );
                })
            .AddApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
}

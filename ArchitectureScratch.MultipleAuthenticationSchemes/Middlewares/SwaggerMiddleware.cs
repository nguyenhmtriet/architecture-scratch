using ArchitectureScratch.MultipleAuthenticationSchemes.Extensions;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Middlewares;

public static class SwaggerMiddleware
{
    public static IApplicationBuilder UseSwaggerMiddleware(this WebApplication app)
    {
        if (!app.Configuration.IsSwaggerEnabled()) return app;

        return app
            .UseSwagger()
            .UseSwaggerUI();
    }
}

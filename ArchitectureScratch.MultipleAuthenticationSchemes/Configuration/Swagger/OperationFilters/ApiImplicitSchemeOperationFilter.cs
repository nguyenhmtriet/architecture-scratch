using ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Swagger.OperationFilters;

public class ApiImplicitSchemeOperationFilter(IOptions<SecurityConfiguration> securityOptions)
    : IOperationFilter
{
    private readonly SecurityConfiguration _securityConfiguration = securityOptions.Value;

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAuthorize =
            context.MethodInfo.DeclaringType?.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() == true ||
            context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

        if (!hasAuthorize) return;

        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = SwaggerGenOptionsConfigure.ImplicitSecuritySchemeName
                        }
                    },
                    _securityConfiguration.CustomIdentityServer.AllowedScopes
                }
            }
        };
    }
}

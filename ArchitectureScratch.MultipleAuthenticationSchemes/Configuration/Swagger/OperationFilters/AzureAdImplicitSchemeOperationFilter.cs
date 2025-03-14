using ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Swagger.OperationFilters;

public class AzureAdImplicitSchemeOperationFilter(IOptions<SecurityConfiguration> securityOptions)
    : IOperationFilter
{
    private readonly SecurityConfiguration _securityConfiguration = securityOptions.Value;

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authorizeAttribute =
            context.MethodInfo.DeclaringType?.GetCustomAttributes(true).OfType<AuthorizeAttribute>().FirstOrDefault() ??
            context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().FirstOrDefault();

        if (authorizeAttribute == null) return;

        if (authorizeAttribute.AuthenticationSchemes == null ||
            !authorizeAttribute.AuthenticationSchemes.Contains(AzureAdDefaults.AuthenticationScheme)) return;

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
                            Id = SwaggerGenOptionsConfigure.AzureAdImplicitSecuritySchemeName
                        }
                    },
                    _securityConfiguration.AzureAd.AllowedScopes
                }
            }
        };
    }
}

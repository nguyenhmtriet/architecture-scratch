using ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;
using ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Swagger.OperationFilters;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Swagger;

public class SwaggerGenOptionsConfigure(IOptions<SecurityConfiguration> securityOptions)
    : IConfigureOptions<SwaggerGenOptions>
{
    private readonly SecurityConfiguration _securityConfiguration = securityOptions.Value;

    public const string ImplicitSecuritySchemeName = "CustomIdentityServer implicit";
    public const string AzureAdImplicitSecuritySchemeName = "AzureAd implicit";

    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc(SwaggerAreas.UserWorkspace,
            new() { Title = $"User workspace APIs - {SwaggerAreas.UserWorkspace}", Version = "v1" });
        options.SwaggerDoc(SwaggerAreas.CustomerWorkspace,
            new() { Title = $"Customer workspace APIs - {SwaggerAreas.CustomerWorkspace}", Version = "v1" });

        options.UseAllOfToExtendReferenceSchemas();
        options.SupportNonNullableReferenceTypes();
        
        options.MapType<DateOnly>(() => new OpenApiSchema 
        { 
            Type = "string", 
            Format = "date",
            Example = OpenApiAnyFactory.CreateFromJson("\"2021-09-01\"")
        });
        options.MapType<TimeOnly>(() => new OpenApiSchema 
        { 
            Type = "string", 
            Format = "time", 
            Example = OpenApiAnyFactory.CreateFromJson("\"13:45:42.0000000\"") 
        }); 
        
        
        options.CustomSchemaIds(type => type.FullName);

        options.AddSecurityDefinition(ImplicitSecuritySchemeName, new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri($"{_securityConfiguration.CustomIdentityServer.Authority}/connect/authorize"),
                    Scopes = _securityConfiguration.CustomIdentityServer.AllowedScopes
                        .ToDictionary(scope => scope, scope => string.Empty)
                }
            }
        });

        options.AddSecurityDefinition(AzureAdImplicitSecuritySchemeName, new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    AuthorizationUrl =
                        new Uri(
                            $"{_securityConfiguration.AzureAd.Authority}/{_securityConfiguration.AzureAd.TenantId}/oauth2/v2.0/authorize"),
                    TokenUrl = new Uri(
                        $"{_securityConfiguration.AzureAd.Authority}/{_securityConfiguration.AzureAd.TenantId}/oauth2/v2.0/token"),
                    Scopes = _securityConfiguration.AzureAd.AllowedScopes
                        .ToDictionary(scope => scope, scope => string.Empty)
                }
            }
        });

        options.OperationFilter<ApiImplicitSchemeOperationFilter>();
        options.OperationFilter<AzureAdImplicitSchemeOperationFilter>();
    }
}

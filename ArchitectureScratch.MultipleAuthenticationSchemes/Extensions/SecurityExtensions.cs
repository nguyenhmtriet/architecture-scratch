using System.Net;
using ArchitectureScratch.MultipleAuthenticationSchemes.Configuration;
using ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;
using ArchitectureScratch.MultipleAuthenticationSchemes.Security.Requirements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Extensions;

public static class SecurityExtensions
{
    public static IServiceCollection ConfigureSecurity(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services
            .AddOptions<SecurityConfiguration>()
            .Bind(configuration.GetSection("Security"))
            .ValidateOnStart()
            .ValidateDataAnnotations();

        using var scope = services.BuildServiceProvider().CreateScope();
        var securityConfig = scope
            .ServiceProvider
            .GetRequiredService<IOptions<SecurityConfiguration>>()
            .Value;
        var appSettingsConfig = scope
            .ServiceProvider
            .GetRequiredService<IOptions<AppSettings>>()
            .Value;

        ConfigureAuthentication(services, environment, securityConfig, appSettingsConfig);
        ConfigureAuthorization(services, securityConfig);
        return services;
    }

    private static void ConfigureAuthentication(
        IServiceCollection services,
        IWebHostEnvironment environment,
        SecurityConfiguration securityConfig,
        AppSettings appSettingsConfig)
    {
        var authenticationBuilder = services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

        authenticationBuilder.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, "JWT Bearer", options =>
        {
            options.Authority = securityConfig.CustomIdentityServer.Authority;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidAudiences = securityConfig.CustomIdentityServer.Audiences,
                ValidIssuer = securityConfig.CustomIdentityServer.Authority
            };
        });


        authenticationBuilder.AddMicrosoftIdentityWebApi(bearerOptions =>
        {
            bearerOptions.Authority =
                $"{securityConfig.AzureAd.Authority}/{securityConfig.AzureAd.TenantId}/oauth2/v2.0";
            bearerOptions.MetadataAddress =
                $"{securityConfig.AzureAd.Authority}/{securityConfig.AzureAd.TenantId}/v2.0/.well-known/openid-configuration";
            bearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidAudiences = securityConfig.AzureAd.Audiences,
                ValidIssuer = $"{securityConfig.AzureAd.Issuer}/{securityConfig.AzureAd.TenantId}/",
            };
        }, microsoftIdentityOptions =>
        {
            microsoftIdentityOptions.Instance = $"{securityConfig.AzureAd.Authority}/";
            microsoftIdentityOptions.ClientId = securityConfig.AzureAd.ClientId;
            microsoftIdentityOptions.TenantId = securityConfig.AzureAd.TenantId;
        }, AzureAdDefaults.AuthenticationScheme);


        authenticationBuilder.AddCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.SlidingExpiration = false;
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.SecurePolicy = environment.IsDevelopment()
                ? CookieSecurePolicy.None
                : CookieSecurePolicy.Always;

            // Cookie key name in the browser
            options.Cookie.Name = "architecture-scratch.multi-schemes.sso";
            var accountSignInPageUrl = GetAccountSignInPageUrl(appSettingsConfig, securityConfig);

            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.Redirect(
                    $"{accountSignInPageUrl}&{CookieAuthenticationConsts.SignedRedirectionUrlQueryParameter}={WebUtility.UrlEncode(context.Request.Path)}");
                return Task.CompletedTask;
            };
        });
    }

    private static void ConfigureAuthorization(IServiceCollection services, SecurityConfiguration securityConfig)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(nameof(CustomIdentityServerSecurityPolices.ProductReaderPolicy),
                policy =>
                {
                    policy.RequireClaim("scope", securityConfig.CustomIdentityServer.Policies.ProductReaderPolicy.ScopeNames);
                });

            options.AddPolicy(nameof(AzureAdPolices.AdminCustomerPolicy),
                policy =>
                {
                    policy.AddRequirements(
                        new AdminCustomerAuthorizationRequirement(securityConfig.AzureAd.Policies.AdminCustomerPolicy
                            .ScopeNames));
                });
        });
    }

    private static string GetAccountSignInPageUrl(AppSettings appSettingsConfig, SecurityConfiguration securityConfig)
    {
        var customIdSConfig = securityConfig.CustomIdentityServer;
        var loginPageUrl = WebUtility.UrlEncode(
            $"{customIdSConfig.Authority}/connect/authorize?response_type=token&client_id={customIdSConfig.ClientId}&redirect_uri={appSettingsConfig.SelfUrl}&scope={string.Join(' ', customIdSConfig.AllowedScopes)}"
        );

        return
            $"{appSettingsConfig.SelfUrl}/account/signin?{CookieAuthenticationConsts.AuthorityUrlQueryParameter}={loginPageUrl}";
    }
}
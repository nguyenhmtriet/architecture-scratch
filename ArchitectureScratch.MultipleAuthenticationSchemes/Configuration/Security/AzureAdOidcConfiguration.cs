using System.ComponentModel.DataAnnotations;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;

public class AzureAdOidcConfiguration : OpenIdConnectConfiguration
{
    [Required]
    public required string Issuer { get; set; }

    [Required]
    public required string TenantId { get; set; }

    [Required]
    public required string ClientId { get; set; }

    [Required]
    public required AzureAdPolices Policies { get; set; }
}

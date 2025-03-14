using System.ComponentModel.DataAnnotations;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;

public class SecurityConfiguration
{
    [Required]
    public required CustomIdentityServerConfiguration CustomIdentityServer { get; set; }

    [Required]
    public required AzureAdOidcConfiguration AzureAd { get; set; }
}

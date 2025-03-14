using System.ComponentModel.DataAnnotations;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;

public class OpenIdConnectConfiguration
{
    [Required]
    public string Authority { get; set; }

    [Required]
    public string[] AllowedScopes { get; set; }

    [Required]
    public string[] Audiences { get; set; }
}

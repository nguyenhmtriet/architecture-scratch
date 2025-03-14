using System.ComponentModel.DataAnnotations;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;

public class CustomIdentityServerConfiguration : OpenIdConnectConfiguration
{
    [Required]
    public CustomIdentityServerSecurityPolices Policies { get; set; }
    
    [Required]
    public string ClientId { get; set; }
}

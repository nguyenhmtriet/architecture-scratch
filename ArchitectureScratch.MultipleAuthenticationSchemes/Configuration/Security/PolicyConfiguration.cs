using System.ComponentModel.DataAnnotations;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;

public class PolicyConfiguration
{
    [Required]
    public string[] ScopeNames { get; set; }

    [Required]
    public string DisplayName { get; set; }
}

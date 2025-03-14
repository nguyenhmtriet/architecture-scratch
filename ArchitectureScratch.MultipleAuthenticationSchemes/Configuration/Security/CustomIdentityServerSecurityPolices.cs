using System.ComponentModel.DataAnnotations;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;

public class CustomIdentityServerSecurityPolices
{
    [Required]
    public required PolicyConfiguration ProductReaderPolicy { get; set; }
}

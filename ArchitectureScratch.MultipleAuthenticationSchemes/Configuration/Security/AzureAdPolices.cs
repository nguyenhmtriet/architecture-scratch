using System.ComponentModel.DataAnnotations;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;

public class AzureAdPolices
{
    [Required]
    public required PolicyConfiguration AdminCustomerPolicy { get; set; }
}

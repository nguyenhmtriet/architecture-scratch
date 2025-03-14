using System.ComponentModel.DataAnnotations;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Configuration;

public class AppSettings
{
    [Required]
    public required string SelfUrl { get; set; }
}

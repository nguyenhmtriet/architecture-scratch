using System.ComponentModel.DataAnnotations;

namespace ArchitectureScratch.NSwagHttpClients.Configuration;

public class ApiClientConfiguration
{
    [Required]
    public string BaseUrl { get; set; } = default!;
}
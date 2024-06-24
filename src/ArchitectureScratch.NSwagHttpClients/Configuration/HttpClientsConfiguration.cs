using System.ComponentModel.DataAnnotations;

namespace ArchitectureScratch.NSwagHttpClients.Configuration;

public class HttpClientsConfiguration
{
    [Required]
    public ApiClientConfiguration LoggingApi { get; set; }
}
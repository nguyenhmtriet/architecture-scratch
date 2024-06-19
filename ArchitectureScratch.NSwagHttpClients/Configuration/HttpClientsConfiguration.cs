using System.ComponentModel.DataAnnotations;

namespace ArchitectureScratch.HttpClients.Configuration;

public class HttpClientsConfiguration
{
    [Required]
    public ApiClientConfiguration LoggingApi { get; set; }
}
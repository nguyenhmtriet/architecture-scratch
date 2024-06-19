﻿using System.ComponentModel.DataAnnotations;

namespace ArchitectureScratch.HttpClients.Configuration;

public class ApiClientConfiguration
{
    [Required]
    public string BaseUrl { get; set; } = default!;
}
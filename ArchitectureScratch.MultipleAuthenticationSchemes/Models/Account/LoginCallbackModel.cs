using System.ComponentModel.DataAnnotations;
using ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Models.Account;

public class LoginCallbackModel
{
    [FromQuery(Name = "access_token")]
    [Required]
    public required string AccessToken { get; set; }

    [FromQuery(Name = "token_type")]
    [Required]
    public required string TokenType { get; set; }

    [FromQuery(Name = "expires_in")]
    [Required]
    public required int ExpiresIn { get; set; }

    [FromQuery(Name = "scope")] [Required] public required string Scope { get; set; }

    [FromQuery(Name = CookieAuthenticationConsts.SignedRedirectionUrlQueryParameter)]
    public string? SignedRedirectionUrl { get; set; }
}
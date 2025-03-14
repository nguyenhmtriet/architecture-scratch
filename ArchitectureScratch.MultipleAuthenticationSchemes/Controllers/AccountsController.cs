using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Authorize(
    AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
[Route("api/v{version:apiVersion}/[controller]")]
public class AccountsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken ct)
    {
        await Task.Delay(1000, ct);
        return Ok(new
        {
            IsAuthenticated = HttpContext.User?.Identity?.IsAuthenticated,
            Scopes = HttpContext.User?.Claims.Select(c => new { c.Type, c.Value }).ToArray()
        });
    }
}
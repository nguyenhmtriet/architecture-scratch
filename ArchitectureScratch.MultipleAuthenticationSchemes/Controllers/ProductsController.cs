using ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Controllers;

[ApiController]
[ApiVersion("1.0")]
// No authentication scheme specified, it uses default scheme in security configuration,
// that is JWTBearerHandler against to the custom identity server
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync(CancellationToken ct)
    {
        await Task.Delay(1000, ct);
        return Ok(new
        {
            IsAuthenticated = HttpContext.User?.Identity?.IsAuthenticated,
            Scopes = HttpContext.User?.Claims.Select(c => new { c.Type, c.Value }).ToArray()
        });
    }
    
    [HttpGet]
    // No authentication scheme specified, it uses default scheme in security configuration,
    // that is JWTBearerHandler against to the custom identity server
    [Authorize(Policy = nameof(CustomIdentityServerSecurityPolices.ProductReaderPolicy))]
    public async Task<IActionResult> GetProductsAsync(CancellationToken ct)
    {
        await Task.Delay(1000, ct);
        return Ok(new
        {
            IsAuthenticated = HttpContext.User?.Identity?.IsAuthenticated,
            Scopes = HttpContext.User?.Claims.Select(c => new { c.Type, c.Value }).ToArray()
        });
    }
}
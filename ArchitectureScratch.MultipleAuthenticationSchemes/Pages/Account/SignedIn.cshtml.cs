using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;
using ArchitectureScratch.MultipleAuthenticationSchemes.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Pages.Account;

public class SignedIn : PageModel
{
    public bool IsAuthenticated { get; private set; }

    public async Task OnGetAsync([FromQuery] LoginCallbackModel model, CancellationToken ct)
    {
        var (principalCookie, authenticationProps) = GetClaimPrincipal(
            model.AccessToken,
            model.ExpiresIn);
        if (principalCookie == null || authenticationProps == null) return;

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principalCookie,
            authenticationProps);

        IsAuthenticated = principalCookie.Identity?.IsAuthenticated ?? false;

        Request.Cookies.TryGetValue(CookieAuthenticationConsts.SignedRedirectionUrlQueryParameter, out var redirectUrl);
        if (!string.IsNullOrWhiteSpace(redirectUrl))
        {
            Response.Redirect(redirectUrl);
            Response.Cookies.Delete(CookieAuthenticationConsts.SignedRedirectionUrlQueryParameter);
        }
    }

    private (ClaimsPrincipal? ClaimsPrincipal, AuthenticationProperties? AuthenticationProperties) GetClaimPrincipal(
        string accessToken,
        int expiresInSeconds)
    {
        var jwtSecurityHandler = new JwtSecurityTokenHandler();
        if (!jwtSecurityHandler.CanReadToken(accessToken)) return (null, null);

        var jwtToken = jwtSecurityHandler.ReadJwtToken(accessToken);
        var claims = jwtToken.Claims.Select(claim => new Claim(claim.Type, claim.Value));

        var principalCookie =
            new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
        var authenticationProps = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(expiresInSeconds), IsPersistent = true,
        };
        authenticationProps.StoreTokens([
            new() { Name = CookieAuthenticationConsts.AccessTokenKey, Value = accessToken }
        ]);

        return (principalCookie, authenticationProps);
    }
}

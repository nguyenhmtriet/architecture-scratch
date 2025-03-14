using ArchitectureScratch.MultipleAuthenticationSchemes.Configuration.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Pages.Account;

public class SigningIn : PageModel
{
    public void OnGet(
        [FromQuery(Name = CookieAuthenticationConsts.AuthorityUrlQueryParameter)]
        string authorityUrl,
        [FromQuery(Name = CookieAuthenticationConsts.SignedRedirectionUrlQueryParameter)]
        string signedRedirectionUrl)
    {
        Response.Cookies.Append(CookieAuthenticationConsts.SignedRedirectionUrlQueryParameter, signedRedirectionUrl,
            new() { HttpOnly = true });

        Response.Redirect(authorityUrl);
    }
}

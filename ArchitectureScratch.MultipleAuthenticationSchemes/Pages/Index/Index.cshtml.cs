using ArchitectureScratch.MultipleAuthenticationSchemes.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Pages.Index;

public class Index(IOptions<AppSettings> appSettingsOption) : PageModel
{
    public string SelfUrl => appSettingsOption.Value.SelfUrl;
}


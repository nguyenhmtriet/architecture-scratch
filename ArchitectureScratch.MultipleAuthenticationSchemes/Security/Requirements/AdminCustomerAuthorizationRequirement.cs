using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;

namespace ArchitectureScratch.MultipleAuthenticationSchemes.Security.Requirements;

public class AdminCustomerAuthorizationRequirement(string[] requiredAllScopes)
    : AuthorizationHandler<AdminCustomerAuthorizationRequirement>,
        IAuthorizationRequirement
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AdminCustomerAuthorizationRequirement requirement)
    {
        var scopes = context.User.Claims.Where(claim => claim.Type == ClaimConstants.Scope).ToArray();
        var scopeValues = scopes.Length == 1
            ? scopes[0].Value.Split(' ')
            : scopes.Select(scope => scope.Value).ToArray();

        var isMatchedAll = requiredAllScopes.All(scope =>
            scopeValues.Contains(scope, StringComparer.InvariantCultureIgnoreCase));

        if (isMatchedAll) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
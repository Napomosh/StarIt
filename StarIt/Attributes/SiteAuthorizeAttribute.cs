using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StarIt.Bl;

namespace StarIt.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class SiteAuthorizeAttribute(bool requireAdmin = false) : Attribute, IAsyncAuthorizationFilter
{
    private readonly bool requireAdmin = requireAdmin;
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        ICurrentUser? currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>();
        if (currentUser == null)
            throw new Exception("ICurrentUser is undefined.");
        
        if (!await currentUser.IsAuthenticated())
            context.Result = new RedirectResult("/login");

        if (requireAdmin && !await currentUser.IsAdmin())
            context.Result = new RedirectResult("/");
    }
}
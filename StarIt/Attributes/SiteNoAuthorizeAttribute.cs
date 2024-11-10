using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StarIt.Bl;

namespace StarIt.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class SiteNoAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        ICurrentUser? currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>();
        if (currentUser == null)
            throw new Exception("ICurrentUser is undefined.");

        if (await currentUser.IsAuthenticated())
            context.Result = new RedirectResult("/");
    }
}
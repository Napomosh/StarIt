using Microsoft.AspNetCore.Mvc;
using StarIt.Bl;

namespace StarIt.ViewComponents;

public class AdminMenuViewComponent(ICurrentUser currentUser) : ViewComponent
{
    private readonly ICurrentUser currentUser = currentUser;
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        bool isAdmin = await currentUser.IsAdmin();
        return View("Index", isAdmin);
    }
}
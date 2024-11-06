using Microsoft.AspNetCore.Mvc;
using StarIt.Bl;

namespace StarIt.ViewComponents;

public class MainMenuViewComponent(ICurrentUser currentUser) : ViewComponent
{
    private readonly ICurrentUser currentUser = currentUser;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        bool isLoggedIn = await currentUser.IsAuthenticated();
        return View("Index", isLoggedIn);
    }
}
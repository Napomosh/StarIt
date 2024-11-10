using StarIt.Bl.Auth;
using StarIt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using StarIt.Attributes;
using StarIt.Tools;

namespace StarIt.Controllers.Login;

[Controller]
public class LoginController(IAuth auth) : Controller
{
    private readonly IAuth auth = auth;

    [HttpGet]
    [Route("/login")]
    [SiteNoAuthorize]
    public IActionResult Login()
    {
        return View("Login", new UserViewModel());
    }

    [HttpGet]
    [Route("/logout")]
    [SiteAuthorize]
    public IActionResult Logout()
    {
        auth.Logout();
        return Redirect("/");
    }

    [HttpPost]
    [Route("/login")]
    [SiteNoAuthorize]
    public async Task<IActionResult> LoginPost(UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (await auth.Login(model.Email, model.Password, model.RememberMe == true))
                return Redirect("/");
        }
        return View("Login", model);
    }
}
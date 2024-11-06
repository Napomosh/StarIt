using StarIt.Bl.Auth;
using StarIt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using StarIt.Tools;

namespace StarIt.Controllers.Login;

public class LoginController(IAuth auth) : Controller
{
    private readonly IAuth auth = auth;

    [HttpGet]
    [Route("/login")]
    public IActionResult Login()
    {
        return View("Login", new UserViewModel());
    }

    [HttpGet]
    [Route("/logout")]
    public IActionResult Logout()
    {
        auth.Logout();
        return Redirect("/");
    }

    [HttpPost]
    [Route("/login")]
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
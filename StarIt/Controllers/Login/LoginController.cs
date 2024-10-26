using StarIt.Bl.Auth;
using StarIt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using StarIt.Tools;

namespace StarIt.Controllers.Login;

public class LoginController(IAuth auth) : Controller
{
    private readonly IAuth _auth = auth;

    [HttpGet]
    [Route("/login")]
    public IActionResult Login()
    {
        return View("Login", new UserViewModel());
    }

    [HttpPost]
    [Route("/login")]
    public async Task<IActionResult> LoginPost(UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (await _auth.Login(model.Email, model.Password))
            {
                ViewData.Add(ViewDataConstants.VIEW_DATA_IS_LOGGED, true);
                ViewData.Add(ViewDataConstants.VIEW_DATA_USER_NICKNAME, model.Nickname);
                return Redirect("/");
            }
        }
        return View("Login", model);
    }
}
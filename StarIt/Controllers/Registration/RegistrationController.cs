using StarIt.Bl.Auth;
using StarIt.Models;
using StarIt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using StarIt.Attributes;
using StarIt.Tools;

namespace StarIt.Controllers.Registration;

[Controller]
[SiteNoAuthorize]
public class RegistrationController(IAuth auth) : Controller
{
    private readonly IAuth auth = auth;
    
    [HttpGet]
    [Route("/register")]
    public IActionResult Register()
    {
        return View("Register", new UserViewModel());
    }

    [HttpPost]
    [Route("/register")]
    public async Task<IActionResult> RegisterPost(UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            UserModel userModel = Mappers.RegistrationMapper.MapUserViewModelToDataModel(model);
            if (await auth.IsEmailExist(model.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View("Register", model);
            }
            if (await auth.RegisterUser(userModel) != Guid.Empty)
                await auth.Login(model.Email, model.Password);
            
            return Redirect("/");
        }
        ViewBag.Title = "Registration";
        return View("Register", model);
    }
}
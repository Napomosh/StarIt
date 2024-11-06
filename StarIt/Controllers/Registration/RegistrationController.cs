using StarIt.Bl.Auth;
using StarIt.Models;
using StarIt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using StarIt.Tools;

namespace StarIt.Controllers.Registration;

[Controller]
public class RegistrationController(IAuth auth) : Controller
{
    private readonly IAuth _auth = auth;
    
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
            if (await _auth.IsEmailExist(model.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View("Register", model);
            }
            if (await _auth.RegisterUser(userModel) != Guid.Empty)
                await _auth.Login(userModel.Email, userModel.Password);
            
            return Redirect("/");
        }
        ViewBag.Title = "Registration";
        return View("Register", model);
    }
}
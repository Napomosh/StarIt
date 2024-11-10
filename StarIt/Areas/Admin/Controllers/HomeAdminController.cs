using Microsoft.AspNetCore.Mvc;
using StarIt.Attributes;

namespace StarIt.Areas.Admin.Controllers;

[Area("Admin")]
[SiteAuthorize(true)]
public class HomeAdminController : Controller
{
    [HttpGet]
    [Route("/admin")]
    [Route("/admin/index")]
    public IActionResult Index()
    {
        return View();
    }
}
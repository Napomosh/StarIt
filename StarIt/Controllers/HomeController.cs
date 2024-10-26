using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StarIt.Bl;
using StarIt.Models;

namespace StarIt.Controllers;

public class HomeController(ILogger<HomeController> logger, ICurrentUser currentUser) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly ICurrentUser _currentUser = currentUser;

    public IActionResult Index()
    {
        return View(_currentUser.IsAuthenticated());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
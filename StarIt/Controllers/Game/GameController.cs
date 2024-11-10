using Microsoft.AspNetCore.Mvc;
using StarIt.Models;

namespace StarIt.Controllers.Game;

public class GameController : Controller
{
    [HttpGet]
    [Route("/games/{gameId}")]
    public IActionResult ShowGameView(string gameId)
    {
        GameModel model = new GameModel();
        return View("GameView", model);
    }

    [HttpGet]
    [Route("/games")]
    public IActionResult ShowGameList()
    {
        return View("GameList");
    }
}
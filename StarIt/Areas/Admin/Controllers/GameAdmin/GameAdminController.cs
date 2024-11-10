using Microsoft.AspNetCore.Mvc;
using StarIt.Attributes;
using StarIt.Bl.Game;
using StarIt.Mappers;
using StarIt.Models;
using StarIt.ViewModels;

namespace StarIt.Areas.Admin.Controllers.GameAdmin;

[Area("Admin")]
[SiteAuthorize(true)]
public class GameAdminController(IGameBl gameBl) : Controller
{
    private readonly IGameBl gameBl = gameBl;
    
    [HttpGet]
    [Route("/games/edit/{gameId?}")]
    public IActionResult ShowGameEdit(string? gameId)
    {
        return View("GameEdit", new GameViewModel());
    }

    [HttpPost]
    [Route("/games/edit")]
    public async Task<IActionResult> SaveGameCard(GameViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            if (await gameBl.CreateGame(GameModelsMapper.MapGameViewModelToDataModel(viewModel)))
                return Redirect("/");
            return View("GameEdit", viewModel);
        }
        return View("GameEdit", viewModel);
    }
}
using StarIt.Models;

namespace StarIt.Bl.Game;

public interface IGameBl
{
    public Task<bool> CreateGame(string title, string description, string imagesPath);
    public Task<bool> CreateGame(GameModel model);
}
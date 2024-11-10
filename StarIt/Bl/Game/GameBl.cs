using StarIt.Dal.Games;
using StarIt.Models;

namespace StarIt.Bl.Game;

public class GameBl(IGameDal gameDal) : IGameBl
{
    private readonly IGameDal gameDal = gameDal;


    public async Task<bool> CreateGame(string title, string description, string imagesPath)
    {
        GameModel gameModel = new GameModel
        {
            Title = title,
            Description = description,
            ImagesPath = imagesPath,
            Rate = 0
        };
        
        return await CreateGame(gameModel);
    }

    public async Task<bool> CreateGame(GameModel model)
    {
        return await gameDal.CreateGame(model) != Guid.Empty;
    }
}
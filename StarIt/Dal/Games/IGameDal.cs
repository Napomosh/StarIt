using StarIt.Models;

namespace StarIt.Dal.Games;

public interface IGameDal
{
    public Task<Guid> CreateGame(GameModel model);
    public Task<GameModel> GetGame(Guid gameId);
}
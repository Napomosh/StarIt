using StarIt.Models;

namespace StarIt.Dal.Games;

public interface IGameCardDal
{
    public Task<ulong> CreateGame(GameModel model);
    public Task<GameModel> GetGame(ulong gameId);
}
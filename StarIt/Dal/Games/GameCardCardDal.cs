using StarIt.Models;

namespace StarIt.Dal.Games;

public class GameCardCardDal : IGameCardDal
{
    public async Task<ulong> CreateGame(GameModel model)
    {
        const string sql = """
                           insert into app_game_card(title, description, rate, images_path) 
                              values (@Title, @Description, @Rate, @ImagesPath)
                           returning gameid;
                           """;
        ulong gameCardId = await DbHelper.QueryScalarAsync<ulong>(sql, model);
        return gameCardId;
    }

    public async Task<GameModel> GetGame(ulong gameId)
    {
        const string sql = """
                           select gameid, title, description, rate, images_path from app_game_card where gameid = @GameId;
                           """;
        var games = await DbHelper.QueryAsync<GameModel>(sql, new { GameId = gameId });
        return games.FirstOrDefault() ?? new GameModel();
    }
}
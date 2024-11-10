using StarIt.Models;

namespace StarIt.Dal.Games;

public class GameDal : IGameDal
{
    public async Task<Guid> CreateGame(GameModel model)
    {
        const string sql = """
                           set @InsertionId = uuid_to_bin(uuid(), false);
                           insert into app_game(GameId, title, description, rate, images_path) 
                              values (@InsertionId, @Title, @Description, @Rate, @ImagesPath);
                           select if(row_count() > 0, @InsertionId, null) as uuid;
                           """;

        return new Guid(await DbHelper.QueryScalarAsync<byte[]>(sql, model));
    }

    public async Task<GameModel> GetGame(Guid gameId)
    {
        const string sql = """
                           select gameid, title, description, rate, images_path from app_game where gameid = @GameId;
                           """;
        var games = await DbHelper.QueryAsync<GameModel>(sql, new { GameId = gameId });
        return games.FirstOrDefault() ?? new GameModel();
    }
}
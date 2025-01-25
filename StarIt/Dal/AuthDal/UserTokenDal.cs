namespace StarIt.Dal.AuthDal;

public class UserTokenDal : IUserTokenDal
{
    public async Task<Guid> Create(Guid userId)
    {
        const string sql = """
                     INSERT INTO user_token (userid, created) VALUES (@userid, NOW())
                     RETURNING tokenId;
                     """;
        
        Guid tokenGuid = await DbHelper.QueryScalarAsync<Guid>(sql, new { userid = userId, });
        Console.Out.WriteLine($"Token with id: {tokenGuid} was created.");
        return tokenGuid;
    }

    public async Task<Guid> GetUserId(Guid tokenId)
    {
        const string sql = """
                           select userid from user_token where tokenid = @tokenid;
                           """;
        Guid userBinId = await DbHelper.QueryScalarAsync<Guid>(sql, new { tokenid = tokenId });

        return userBinId;
    }

    public async Task Delete(Guid tokenId)
    {
        const string sql = """
                           delete from user_token where tokenid = @tokenid;
                           """;
        await DbHelper.ExecuteAsync(sql, new { tokenid = tokenId });
    }

    public async Task Delete(string tokenId)
    {
        Guid.TryParse(tokenId, out Guid token);
        await Delete(token);
    }
}
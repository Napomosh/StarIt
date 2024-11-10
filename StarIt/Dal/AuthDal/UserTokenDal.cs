namespace StarIt.Dal.AuthDal;

public class UserTokenDal : IUserTokenDal
{
    public async Task<Guid> Create(byte[] userId)
    {
        Guid tokenGuid = Guid.NewGuid();
        const string sql = """
                     INSERT INTO user_token (tokenid, userid, created) VALUES (UUID_TO_BIN(@tokenid, false), @userid, NOW());
                     """;
        
        await DbHelper.ExecuteAsync(sql, new
        {
            tokenid = tokenGuid,
            userid = userId,
        });
        
        return tokenGuid;
    }

    public async Task<Guid> GetUserId(byte[] tokenId)
    {
        const string sql = """
                           select userid from user_token where tokenid = uuid_to_bin(@tokenid);
                           """;
        byte[]? userBinId = await DbHelper.QueryScalarAsync<byte[]>(sql, new { tokenid = new Guid(tokenId) });

        return userBinId is null ? Guid.Empty : new Guid(userBinId);
    }

    public async Task Delete(byte[] tokenId)
    {
        const string sql = """
                           delete from user_token where tokenid = uuid_to_bin(@tokenid);
                           """;
        await DbHelper.ExecuteAsync(sql, new { tokenid = new Guid(tokenId) });
    }
}
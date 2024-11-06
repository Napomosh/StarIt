namespace StarIt.Dal.AuthDal;

public class UserTokenDal : IUserTokenDal
{
    public async Task<Guid> Create(byte[] userId)
    {
        Guid tokenGuid = Guid.NewGuid();
        const string sql = """
                     INSERT INTO user_token (token_id, user_id, created) VALUES (UUID_TO_BIN(@token_id, false), @user_id, NOW());
                     """;
        
        await DbHelper.ExecuteAsync(sql, new
        {
            token_id = tokenGuid,
            user_id = userId,
        });
        
        return tokenGuid;
    }

    public async Task<Guid> GetUserId(byte[] tokenId)
    {
        const string sql = """
                           select user_id from user_token where token_id = uuid_to_bin(@tokenId);
                           """;
        byte[]? userBinId = await DbHelper.QueryScalarAsync<byte[]>(sql, new { tokenId = new Guid(tokenId) });

        return userBinId is null ? Guid.Empty : new Guid(userBinId);
    }

    public async Task Delete(byte[] tokenId)
    {
        const string sql = """
                           delete from user_token where token_id = uuid_to_bin(@tokenId);
                           """;
        await DbHelper.ExecuteAsync(sql, new { tokenId = new Guid(tokenId) });
    }
}
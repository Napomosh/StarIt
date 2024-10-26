using StarIt.Models;

namespace StarIt.Dal.AuthDal;

public class AuthDal : IAuthDal
{
    public async Task<Guid> CreateUser(UserModel user)
    {
        string sql = """
                     SET @LastUuid = uuid_to_bin(UUID()); 
                     INSERT INTO app_user(UserId, Email, Password, Salt, Status, Nickname) VALUES(@LastUuid, @Email, @Password, @Salt, @Status, @Nickname);
                     SELECT IF(ROW_COUNT() > 0, @LastUuid, null) as uuid;
                     """;
        
        return new Guid(await DbHelper.QueryScalarAsync<byte[]>(sql, user));
    }

    public async Task<UserModel> GetUser(string email)
    {
        string sql = """
                     SELECT UserId, Email, Password, Salt, Status, Nickname FROM app_user WHERE Email = @Email;
                     """;
        var userByteIds = await DbHelper.QueryAsync<UserModel>(sql, new { Email = email });
        return userByteIds.FirstOrDefault() ?? new UserModel();
    }

    public async Task<bool> IsEmailExist(string email)
    {
        var userModel = await GetUser(email);
        return !new Guid(userModel.UserId).Equals(Guid.Empty);
    }
}
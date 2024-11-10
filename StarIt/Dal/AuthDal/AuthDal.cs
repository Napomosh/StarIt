using StarIt.Models;

namespace StarIt.Dal.AuthDal;

public class AuthDal : IAuthDal
{
    public async Task<Guid> CreateUser(UserModel user)
    {
        string sql = """
                     set @InsertionId = uuid_to_bin(uuid(), false);
                     INSERT INTO app_user(UserId, Email, Password, Salt, Status, Nickname) VALUES (@InsertionId, @Email, @Password, @Salt, @Status, @Nickname);
                     SELECT IF(ROW_COUNT() > 0, @InsertionId, null) as uuid;
                     """;
        
        return new Guid(await DbHelper.QueryScalarAsync<byte[]>(sql, user));
    }

    public async Task<UserModel> GetUser(string email)
    {
        string sql = """
                     SELECT UserId, Email, Password, Salt, Status, Nickname FROM app_user WHERE Email = @Email;
                     """;
        var userModels = await DbHelper.QueryAsync<UserModel>(sql, new { Email = email });
        return userModels.FirstOrDefault() ?? new UserModel();
    }

    public async Task<bool> IsEmailExist(string email)
    {
        var userModel = await GetUser(email);
        return !new Guid(userModel.UserId).Equals(Guid.Empty);
    }

    public async Task<IEnumerable<UserRoleModel>> GetRoles(byte[] userId)
    {
        return await DbHelper.QueryAsync<UserRoleModel>(
            """
               select user_roles.roleid, user_roles.abbreviation, user_roles.role_name from app_user_role user_roles 
                    join app_user_role_user user_roles_user on user_roles.roleId = user_roles_user.RoleId
               where user_roles_user.userid = @UserId;
               """, new { UserId = userId });
    }
}
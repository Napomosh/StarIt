using StarIt.Models;

namespace StarIt.Dal.AuthDal;

public class AuthDal : IAuthDal
{
    public async Task<Guid> CreateUser(UserModel user)
    {
        string sql = """
                     INSERT INTO app_user(Email, Password, Salt, Nickname, Status) 
                        VALUES (@Email, @Password, @Salt, @Nickname, @Status)
                     RETURNING userid;
                     """;
        user.UserId = await DbHelper.QueryScalarAsync<Guid>(sql, user);
        Console.WriteLine($"User {user.Email} with id {user.UserId} has been created.");
        return user.UserId;
    }

    public async Task<UserModel> GetUser(string email)
    {
        string sql = """
                     SELECT UserId, Email, Password, Salt, nickname, Status FROM app_user WHERE Email = @Email;
                     """;
        var userModels = await DbHelper.QueryAsync<UserModel>(sql, new { Email = email });
        return userModels.FirstOrDefault() ?? new UserModel();
    }

    public async Task<bool> IsEmailExist(string email)
    {
        var userModel = await GetUser(email);
        return !userModel.UserId.Equals(Guid.Empty);
    }

    public async Task<IEnumerable<UserRoleModel>> GetRoles(Guid userId)
    {
        return await DbHelper.QueryAsync<UserRoleModel>(
            """
               select user_roles.roleid, user_roles.abbreviation, user_roles.roleName from app_user_role user_roles 
                    join app_user_role_user user_roles_user on user_roles.roleId = user_roles_user.RoleId
               where user_roles_user.userid = @UserId;
               """, new { UserId = userId });
    }
}
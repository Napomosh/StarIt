using StarIt.Models;

namespace StarIt.Dal.AuthDal;

public interface IAuthDal
{
    public Task<Guid> CreateUser(UserModel user);
    public Task<UserModel> GetUser(string email);
    public Task<bool> IsEmailExist(string email);
    public Task<IEnumerable<UserRoleModel>> GetRoles(byte[] userId);
}
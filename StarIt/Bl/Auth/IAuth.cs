using StarIt.Models;

namespace StarIt.Bl.Auth;

public interface IAuth
{
    public Task<Guid> RegisterUser(UserModel user);
    public Task<bool> Login(string email, string password, bool rememberMe = false);
    public Task Logout();
    public Task<bool> IsEmailExist(string email);
}
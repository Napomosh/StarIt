using StarIt.Dal.AuthDal;
using StarIt.Models;
using StarIt.Tools;

namespace StarIt.Bl.Auth;

public class Auth(IAuthDal authDal, IEncrypt encrypt, IHttpContextAccessor httpContextAccessor) : IAuth
{
    private readonly IAuthDal _authDal = authDal;
    private readonly IEncrypt _encrypt = encrypt;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    
    public async Task<Guid> RegisterUser(UserModel user)
    {
        user.Salt = Guid.NewGuid().ToString();
        user.Password = _encrypt.HashPassword(user.Password!, user.Salt);
        
        var result = await _authDal.CreateUser(user);
        SetLoginCookie(Convert.ToHexString(user.UserId));
        return result;
    }

    public async Task<bool> Login(string email, string password)
    {
        var user = await _authDal.GetUser(email);

        var result = user.IsEmpty || 
                _encrypt.HashPassword(password, user.Salt).Equals(user.Password);

        if (result)
            SetLoginCookie(Convert.ToHexString(user.UserId));

        return result;
    }

    public void SetLoginCookie(string value)
    {
        _httpContextAccessor.HttpContext?.Session.SetString(AuthConstants.AUTH_USER_ID, "Convert.ToHexString(user.UserId)");
    }

    public async Task<bool> IsEmailExist(string email)
    {
        return await _authDal.IsEmailExist(email);
    }
}
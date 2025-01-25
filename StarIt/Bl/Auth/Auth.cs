using System.Text;
using StarIt.Bl.Common;
using StarIt.Dal.AuthDal;
using StarIt.Models;
using StarIt.Tools;

namespace StarIt.Bl.Auth;

public class Auth(
    IAuthDal authDal,
    IEncrypt encrypt,
    IHttpContextAccessor httpContextAccessor,
    IUserTokenDal userTokenDal,
    IWebCookie webCookie) : IAuth
{
    private readonly IAuthDal authDal = authDal;
    private readonly IEncrypt encrypt = encrypt;
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
    private readonly IUserTokenDal userTokenDal = userTokenDal;
    private readonly IWebCookie webCookie = webCookie;

    public async Task<Guid> RegisterUser(UserModel user)
    {
        if (await IsEmailExist(user.Email))
            return Guid.Empty;
        
        user.Salt = Guid.NewGuid().ToString();
        user.Password = encrypt.HashPassword(user.Password!, user.Salt);
        var result = await authDal.CreateUser(user);
        return result;
    }

    public async Task<bool> Login(string email, string password, bool rememberMe = false)
    {
        var user = await authDal.GetUser(email);
        var result = user.UserId != Guid.Empty && encrypt.HashPassword(password, user.Salt).Equals(user.Password);
        if (!result) 
            return false;
        
        httpContextAccessor.HttpContext?.Session.SetString(AuthConstants.AUTH_USER_ID, user.UserId.ToString());
        if (rememberMe)
        {
            Guid token = await userTokenDal.Create(user.UserId);
            webCookie.AddSecure(AuthConstants.AUTH_REMEMBER_ME_COOKIE, token.ToString()
                , AuthConstants.AUTH_REMEMBER_ME_COOKIE_DAYS);
        }

        var roles = await authDal.GetRoles(user.UserId);
        if (roles.Any(r => r.Abbreviation == AuthConstants.AUTH_ROLE_ADMIN_ABBR))
            httpContextAccessor.HttpContext?.Session.SetString(AuthConstants.AUTH_ROLE_CURRENT, AuthConstants.AUTH_ROLE_ADMIN_ABBR);
        return result;
    }

    public async Task Logout()
    {
        httpContextAccessor.HttpContext?.Session.Clear();
        await  userTokenDal.Delete( webCookie.Get(AuthConstants.AUTH_REMEMBER_ME_COOKIE));
        webCookie.Delete(AuthConstants.AUTH_REMEMBER_ME_COOKIE);
    }

    public async Task<bool> IsEmailExist(string email)
    {
        return await authDal.IsEmailExist(email);
    }
}
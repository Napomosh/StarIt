using StarIt.Bl.Common;
using StarIt.Dal.AuthDal;
using StarIt.Tools;

namespace StarIt.Bl;

public class CurrentUser(IHttpContextAccessor contextAccessor, IWebCookie webCookie, IUserTokenDal userTokenDal
    , IAuthDal authDal) : ICurrentUser
{
    private readonly IHttpContextAccessor httpContextAccessor = contextAccessor;
    private readonly IWebCookie webCookie = webCookie;
    private readonly IUserTokenDal userTokenDal = userTokenDal;
    private readonly IAuthDal authDal = authDal;
    
    public async Task<bool> IsAuthenticated()
    {
        string? userId = httpContextAccessor.HttpContext?.Session.GetString(AuthConstants.AUTH_USER_ID);
        if (userId != null) 
            return true;
        Guid userGuid = await GetUserIdByToken();

        return !userGuid.Equals(Guid.Empty);
    }

    public async Task<Guid> GetUserIdByToken()
    {
        string tokenCookie = webCookie.Get(AuthConstants.AUTH_REMEMBER_ME_COOKIE);
        if (tokenCookie.Equals(string.Empty))
            return Guid.Empty;
        Guid tokenGuid = Guid.Parse(tokenCookie);
        return await userTokenDal.GetUserId(tokenGuid.ToByteArray());
    }

    public async Task<bool> IsAdmin()
    {
        if (httpContextAccessor.HttpContext?.Session.GetString(AuthConstants.AUTH_ROLE_CURRENT)
            is AuthConstants.AUTH_ROLE_ADMIN_ABBR)
            return true;

        var userId = await GetUserIdByToken();
        var roles = await authDal.GetRoles(userId.ToByteArray());
        return roles.Any(r => r.Abbreviation == AuthConstants.AUTH_ROLE_ADMIN_ABBR);
    }
}
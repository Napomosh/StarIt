using Microsoft.AspNetCore.Http;
using StarIt.Bl;
using StarIt.Bl.Auth;
using StarIt.Bl.Common;
using StarIt.Dal.AuthDal;

namespace StarItTest.Common;

public class BaseTest
{
    protected readonly IWebCookie webCookie;
    protected readonly IAuth authBl;
    protected readonly IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
    protected readonly ICurrentUser currentUser;
    
    private readonly IAuthDal authDal = new AuthDal();
    private readonly IEncrypt encrypt = new Encrypt();
    private readonly IUserTokenDal userTokenDal = new UserTokenDal();

    protected BaseTest()
    {
        webCookie = new TestCookie();
        authBl = new Auth(authDal, encrypt, httpContextAccessor, userTokenDal, webCookie);
        currentUser = new CurrentUser(httpContextAccessor, webCookie, userTokenDal);
    }
}
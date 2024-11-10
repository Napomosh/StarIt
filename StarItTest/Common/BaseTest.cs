using Microsoft.AspNetCore.Http;
using StarIt.Bl;
using StarIt.Bl.Auth;
using StarIt.Bl.Common;
using StarIt.Bl.Game;
using StarIt.Dal.AuthDal;
using StarIt.Dal.Games;

namespace StarItTest.Common;

public class BaseTest
{
    protected readonly IWebCookie webCookie;
    protected readonly IAuth authBl;
    protected readonly IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
    protected readonly ICurrentUser currentUser;
    protected readonly IGameBl gameBl;
    
    private readonly IAuthDal authDal = new AuthDal();
    private readonly IGameDal gameDal = new GameDal();
    private readonly IEncrypt encrypt = new Encrypt();
    private readonly IUserTokenDal userTokenDal = new UserTokenDal();

    protected BaseTest()
    {
        webCookie = new TestCookie();
        authBl = new Auth(authDal, encrypt, httpContextAccessor, userTokenDal, webCookie);
        currentUser = new CurrentUser(httpContextAccessor, webCookie, userTokenDal, authDal);
        gameBl = new GameBl(gameDal);
    }
}
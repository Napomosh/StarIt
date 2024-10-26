using StarIt.Tools;

namespace StarIt.Bl;

public class CurrentUser(IHttpContextAccessor contextAccessor) : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor = contextAccessor;
    
    public bool IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext?.Session?.GetString(AuthConstants.AUTH_USER_ID) != null;
    }
}
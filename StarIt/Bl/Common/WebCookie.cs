namespace StarIt.Bl.Common;

public class WebCookie(IHttpContextAccessor httpContextAccessor) : IWebCookie
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    
    public void AddSecure(string key, string value, int days = 0)
    {
        CookieOptions options = new CookieOptions
        {
            Path = "/",
            HttpOnly = true,
            Secure = true
        };
        if (days > 0)
            options.Expires = DateTimeOffset.UtcNow.AddDays(days);
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value, options);
    }

    public void Add(string key, string value, int days = 0)
    {
        CookieOptions options = new CookieOptions
        {
            Path = "/"
        };
        if (days > 0)
            options.Expires = DateTimeOffset.UtcNow.AddDays(days);
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value, options);
}

    public string Get(string key)
    {
        return _httpContextAccessor.HttpContext?.Request.Cookies[key] ?? string.Empty;
    }

    public void Delete(string key)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(key);
    }
}
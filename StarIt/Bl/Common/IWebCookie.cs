namespace StarIt.Bl.Common;

public interface IWebCookie
{
    public void AddSecure(string key, string value, int days = 0);
    public void Add(string key, string value, int days = 0);
    public string Get(string key);
    public void Delete(string key);

}
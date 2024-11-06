namespace StarIt.Bl.Common;

public interface IEncrypt
{
    public string HashPassword(string password, string salt);
}
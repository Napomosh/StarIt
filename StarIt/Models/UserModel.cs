namespace StarIt.Models;

public class UserModel
{
    public byte[] UserId { get; set; } = new byte[16];
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public int Status { get; set; } = 0;

    public bool IsEmpty => Empty();

    private bool Empty()
    {
        return Convert.ToBase64String(UserId).Equals("AAAAAAAAAAAAAAAAAAAAAA==");
    }
}
namespace StarIt.Models;

public class UserModel
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public int Status { get; set; } = 0;
}
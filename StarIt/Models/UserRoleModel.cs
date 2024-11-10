namespace StarIt.Models;

public class UserRoleModel
{
    public int RoleId { get; set; }
    public string Abbreviation { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}
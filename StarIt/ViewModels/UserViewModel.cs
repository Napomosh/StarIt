using System.ComponentModel.DataAnnotations;

namespace StarIt.ViewModels;

public class UserViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;
    
    [Display(Name = "User nickname. By default it is your email address")]
    public string Nickname { get; set; } = string.Empty;
    
    public bool? RememberMe { get; set; }
}
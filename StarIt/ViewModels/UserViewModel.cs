﻿using System.ComponentModel.DataAnnotations;

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
    
    [Required]
    [Display(Name = "User nickname")]
    public string Nickname { get; set; } = string.Empty;
}
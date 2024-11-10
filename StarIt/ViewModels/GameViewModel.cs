using System.ComponentModel.DataAnnotations;

namespace StarIt.ViewModels;

public class GameViewModel
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
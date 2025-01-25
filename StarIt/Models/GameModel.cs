namespace StarIt.Models;

public class GameModel
{
    public ulong GameId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public short Rate { get; set; } = 0;
    public string ImagesPath { get; set; } = string.Empty;
}
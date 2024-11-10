namespace StarIt.Models;

public class GameModel
{
    public byte[] GameId { get; set; } = new byte[16];
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ushort Rate { get; set; } = 0;
    public string ImagesPath { get; set; } = string.Empty;
    
    public bool IsEmpty => Empty();

    private bool Empty()
    {
        return Convert.ToBase64String(GameId).Equals("AAAAAAAAAAAAAAAAAAAAAA==");
    }
    
}
namespace TempleOfDoom.GameLogic.Models
{
    public interface ILocatable
    {
        (int x, int y) Position { get; set; }
    }
}

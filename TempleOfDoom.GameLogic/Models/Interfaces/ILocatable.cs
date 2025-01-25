namespace TempleOfDoom.GameLogic.Models.Interfaces
{
    public interface ILocatable
    {
        (int x, int y) Position { get; set; }
    }
}

using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class Wall : ILocatable
    {
        public (int x, int y) Position { get; set; }
    }
}

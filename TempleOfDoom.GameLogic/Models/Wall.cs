using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class Wall : ILocatable
    {
        public Wall((int x, int y) position) {
            Position = position;
        }
        public (int x, int y) Position { get; set; }
    }
}

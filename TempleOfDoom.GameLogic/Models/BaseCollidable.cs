using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class BaseCollidable : ICollidable
    {
        public Action<ICollidable> OnCollision
        {
            get => c => { };
        }
        public (int x, int y) Position { get; set; }

        public BaseCollidable(int x, int y)
        {
            Position = (x, y);
        }
    }
}

using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class BaseCollidable : ICollidable
    {
        public Action<ICollidable> OnEnter
        {
            get => c => { };
        }

        public Action<ICollidable> OnExit
        {
            get => c => { };
        }

        public (int x, int y) Position { get; set; }

        public BaseCollidable((int x, int y) position)
        {
            Position = position;
        }
    }
}

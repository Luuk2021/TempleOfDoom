using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class BaseCollidable : ICollidable
    {
        public virtual Action<ICollidable> OnEnter
        {
            get => c => { };
        }

        public virtual Action<ICollidable> OnExit
        {
            get => c => { };
        }

        public virtual (int x, int y) Position { get; set; }

        public BaseCollidable((int x, int y) position)
        {
            Position = position;
        }
    }
}

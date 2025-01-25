using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class CollidableDecorator : ICollidable
    {
        protected ICollidable _wrapee;
        public virtual (int x, int y) Position { get => _wrapee.Position; set => _wrapee.Position = value; }
        public virtual Action<ICollidable> OnCollision { get => _wrapee.OnCollision; }
        public CollidableDecorator(ICollidable wrapee)
        {
            _wrapee = wrapee;
        }
    }
}
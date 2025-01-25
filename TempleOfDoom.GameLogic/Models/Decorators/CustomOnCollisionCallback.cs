using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class CustomOnCollisionCallback : CollidableDecorator
    {
        private Action<ICollidable> _callback;
        public override Action<ICollidable> OnCollision
        {
            get => c =>
        {
            _wrapee.OnCollision(c);
            _callback(c);
        };
        }
        public CustomOnCollisionCallback(ICollidable wrapee, Action<ICollidable> callback) : base(wrapee)
        {
            _callback = callback;
        }
    }
}

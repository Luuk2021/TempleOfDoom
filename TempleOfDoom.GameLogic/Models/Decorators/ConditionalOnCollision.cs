using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class ConditionalOnCollision(ICollidable wrapee, Func<ICollidable, bool> condition) : CollidableDecorator(wrapee)
    {
        public override Action<ICollidable> OnCollision
        {
            get => c =>
            {
                if (condition(c))
                {
                    _wrapee.OnCollision(c);
                }
            };
        }
    }
}

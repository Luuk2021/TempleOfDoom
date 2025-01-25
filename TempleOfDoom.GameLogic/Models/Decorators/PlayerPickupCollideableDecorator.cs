using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class PlayerPickupCollideableDecorator : CollidableDecorator, IHoldable
    {
        public override Action<ICollidable> OnCollision
        {
            get => c =>
            {
                _wrapee.OnCollision(c);
                if (c is Player player)
                {
                    player.AddItem(this);
                }
            };
        }
        public PlayerPickupCollideableDecorator(ICollidable wrapee) : base(wrapee)
        {
        }
    }
}

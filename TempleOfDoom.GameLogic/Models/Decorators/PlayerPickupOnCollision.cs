using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class PlayerPickupOnCollision : CollidableDecorator, IHoldable
    {
        public override Action<ICollidable> OnEnter
        {
            get => c =>
            {
                Wrapee.OnEnter(c);
                if (c is Player player)
                {
                    player.AddItem(this);
                }
            };
        }
        public PlayerPickupOnCollision(ICollidable wrapee) : base(wrapee)
        {
        }
    }
}

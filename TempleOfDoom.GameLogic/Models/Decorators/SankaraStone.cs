using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class SankaraStone : PlayerPickupCollideableDecorator
    {
        public SankaraStone(ICollidable wrapee, Action removeCallback) : base(new RemoveCollidableDecorator(wrapee, removeCallback))
        {
        }
    }
}

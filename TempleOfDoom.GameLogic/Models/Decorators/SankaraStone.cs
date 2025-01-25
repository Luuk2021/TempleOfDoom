using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class SankaraStone : PlayerPickupCollideableDecorator
    {
        public SankaraStone(ICollidable wrapee, Action<ICollidable> removeCallback) : base(new CustomOnCollisionCallback(wrapee, removeCallback))
        {
        }
    }
}

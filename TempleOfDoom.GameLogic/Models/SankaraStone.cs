using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class SankaraStone : PlayerPickupOnCollision
    {
        public SankaraStone(ICollidable wrapee) : base(new RemoveOnCollision(wrapee))
        {
        }
    }
}

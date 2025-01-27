using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class Key : PlayerPickupOnCollision
    {
        public int KeyId { get; private set; }
        public Key(ICollidable wrapee, int keyId) : base(new RemoveOnCollision(wrapee))
        {
            KeyId = keyId;
        }
    }
}

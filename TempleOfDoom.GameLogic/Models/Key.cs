using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class Key : PlayerPickupOnCollision
    {
        public string KeyId { get; private set; }
        public Key(ICollidable wrapee, string keyId) : base(new RemoveOnCollision(wrapee))
        {
            KeyId = keyId;
        }
    }
}

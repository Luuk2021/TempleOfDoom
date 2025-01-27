using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class BoobyTrap : DamageOnCollision
    {
        public BoobyTrap(ICollidable collidable, int damage) : base(collidable, damage)
        {
        }
    }
}

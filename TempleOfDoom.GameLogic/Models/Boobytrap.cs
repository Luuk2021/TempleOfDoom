using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class Boobytrap : DamageOnCollision
    {
        public Boobytrap(ICollidable collidable, int damage) : base(collidable, damage)
        {
        }
    }
}

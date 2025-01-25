using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class Boobytrap : DamageOnCollision
    {
        public Boobytrap(ICollidable collidable, int damage) : base(collidable, damage)
        {
        }
    }
}

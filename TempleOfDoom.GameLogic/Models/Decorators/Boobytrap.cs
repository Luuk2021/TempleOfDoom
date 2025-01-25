using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class Boobytrap : PlayerDamageCollidableDecorator
    {
        public Boobytrap(ICollidable collidable, int damage) : base(collidable, damage)
        {
        }
    }
}

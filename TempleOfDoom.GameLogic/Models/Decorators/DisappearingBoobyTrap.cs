

using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class DisappearingBoobyTrap : RemoveCollidableDecorator
    {
        public DisappearingBoobyTrap(ICollidable collidable, int damage, Action callback) : base(new Boobytrap(collidable, damage), callback)
        {
        }
    }
}

using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class DisappearingBoobyTrap : Boobytrap
    {
        public DisappearingBoobyTrap(ICollidable collidable, int damage) : base(new RemoveOnCollision(collidable), damage)
        {
        }
    }
}

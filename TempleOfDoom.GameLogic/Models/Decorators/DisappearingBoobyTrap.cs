using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class DisappearingBoobyTrap : RemoveCollidableDecorator
    {
        public DisappearingBoobyTrap(ICollidable collidable, int damage, Action removeCallBack) : base(new Boobytrap(collidable, damage), removeCallBack)
        {
        }
    }
}

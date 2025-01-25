using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class DisappearingBoobyTrap : CustomOnCollisionCallback
    {
        public DisappearingBoobyTrap(ICollidable collidable, int damage, Action<ICollidable> removeCallBack) : base(new Boobytrap(collidable, damage), removeCallBack)
        {
        }
    }
}

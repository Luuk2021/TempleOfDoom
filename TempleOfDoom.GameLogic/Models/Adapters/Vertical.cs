using CODE_TempleOfDoom_DownloadableContent;
using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;
using TempleOfDoom.GameLogic.Services;


namespace TempleOfDoom.GameLogic.Models.Adapters
{
    public class Vertical : EnemyAdapter
    {
        private const int _health = 3;
        public Vertical(ICollidable collidable, (int x, int y) minPosition, (int x, int y) maxPosition) : base(new VerticallyMovingEnemy(_health, collidable.Position.x, collidable.Position.y, minPosition.y, maxPosition.y))
        {
        }
    }
}

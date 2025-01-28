using CODE_TempleOfDoom_DownloadableContent;
using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;
using TempleOfDoom.GameLogic.Services;


namespace TempleOfDoom.GameLogic.Models.Adapters
{
    public class Horizontal: EnemyAdapter
    {
        private const int _health = 3;
        public Horizontal(ICollidable collidable, (int x, int y) minPosition, (int x, int y) maxPosition) : base(new HorizontallyMovingEnemy(_health, collidable.Position.x, collidable.Position.y, minPosition.x, maxPosition.x))
        {
        }
    }
}

using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class PlayerDamageCollidableDecorator : CollidableDecorator
    {
        public int Damage { get; }
        public override Action<ICollidable> OnCollision { get => c =>
        {
            _wrapee.OnCollision(c);
            if (c is Player player)
            {
                player.TakeDamage(Damage);
            }
        };
        }
        public PlayerDamageCollidableDecorator(ICollidable wrapee, int damage): base(wrapee)
        {
            Damage = damage;
        }
    }

}

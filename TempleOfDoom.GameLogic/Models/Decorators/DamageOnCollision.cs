using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class DamageOnCollision : CollidableDecorator
    {
        public int Damage { get; }
        public override Action<ICollidable> OnCollision
        {
            get => c =>
            {
                _wrapee.OnCollision(c);
                if (c is IDamageable damageable)
                {
                    damageable.TakeDamage(Damage);
                }
            };
        }

        public DamageOnCollision(ICollidable wrapee, int damage): base(wrapee)
        {
            Damage = damage;
        }
    }
}

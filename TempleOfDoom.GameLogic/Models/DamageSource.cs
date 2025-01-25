using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public abstract class DamageSource : ICollidable
    {
        protected abstract int Damage { get; }
        public Action<ICollidable> OnCollision => c =>
        {
            if (c is IDamageable damageable)
                damageable.TakeDamage(Damage);
        };

        public abstract (int x, int y) Position { get; set; }
    }
}

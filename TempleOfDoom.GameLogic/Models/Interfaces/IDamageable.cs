namespace TempleOfDoom.GameLogic.Models.Interfaces
{
    public interface IDamageable : IObservable<int>, ILocatable
    {
        int Health { get; }
        void TakeDamage(int damage);
    }
}

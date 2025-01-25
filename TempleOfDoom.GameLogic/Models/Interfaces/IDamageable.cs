namespace TempleOfDoom.GameLogic.Models.Interfaces
{
    public interface IDamageable : IObservable<int>
    {
        int Health { get; }
        void TakeDamage(int damage);
    }
}

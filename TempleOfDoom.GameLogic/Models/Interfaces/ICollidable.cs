namespace TempleOfDoom.GameLogic.Models.Interfaces
{
    public interface ICollidable : ILocatable
    {
        Action<ICollidable> OnCollision { get; }
    }
}

namespace TempleOfDoom.GameLogic.Models.Interfaces
{
    public interface ICollidable : ILocatable
    {
        Action<ICollidable> OnEnter { get; }
        Action<ICollidable> OnStay { get; }
        Action<ICollidable> OnExit { get; }
    }
}

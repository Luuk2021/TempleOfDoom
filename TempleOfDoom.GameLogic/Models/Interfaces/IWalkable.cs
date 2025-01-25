namespace TempleOfDoom.GameLogic.Models.Interfaces
{
    public interface IWalkable : ILocatable, IObservable<((int x, int y) oldPos, ILocatable)>
    {
        void TryMove(Direction direction, Func<(int x, int y), bool> canMoveTo);
    }
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}

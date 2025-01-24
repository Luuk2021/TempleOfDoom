namespace TempleOfDoom.GameLogic.Models
{
    public interface IWalkable : ILocatable
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

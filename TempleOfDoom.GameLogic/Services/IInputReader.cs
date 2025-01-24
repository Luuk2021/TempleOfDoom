namespace TempleOfDoom.GameLogic.Services
{
    public interface IInputReader
    {
        GameAction GetNextAction();
    }
    public enum GameAction
    {
        None,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Quit
    }
}

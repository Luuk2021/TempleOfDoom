namespace TempleOfDoom.GameLogic.Services
{
    public interface IInputReader
    {
        GameAction GetNextInput();
    }
    public enum GameAction
    {
        None,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Quit,
        Start
    }
}

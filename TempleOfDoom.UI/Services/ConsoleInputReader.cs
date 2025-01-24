using TempleOfDoom.GameLogic.Services;

namespace TempleOfDoom.UI
{
    public class ConsoleInputReader : IInputReader
    {
        Dictionary<ConsoleKey, GameAction> _keyMappings = new()
        {
            { ConsoleKey.UpArrow, GameAction.MoveUp },
            { ConsoleKey.DownArrow, GameAction.MoveDown },
            { ConsoleKey.LeftArrow, GameAction.MoveLeft },
            { ConsoleKey.RightArrow, GameAction.MoveRight },
            { ConsoleKey.Escape, GameAction.Quit }
        };
        public GameAction GetNextInput()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (_keyMappings.ContainsKey(key.Key))
                {
                    return _keyMappings[key.Key];
                }
            }
            return GameAction.None;
        }
    }
}
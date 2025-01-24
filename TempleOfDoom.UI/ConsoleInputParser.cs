using TempleOfDoom.GameLogic.Services;

namespace TempleOfDoom.UI
{
    public class ConsoleInputParser : IInputReader
    {
        Dictionary<ConsoleKey, GameAction> _keyMappings = new()
        {
            { ConsoleKey.W, GameAction.MoveUp },
            { ConsoleKey.S, GameAction.MoveDown },
            { ConsoleKey.A, GameAction.MoveLeft },
            { ConsoleKey.D, GameAction.MoveRight },
            { ConsoleKey.Q, GameAction.Quit }
        };
        public GameAction GetNextAction()
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
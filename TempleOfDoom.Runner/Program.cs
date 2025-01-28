using System.Runtime.InteropServices;
using TempleOfDoom.GameLogic;
using TempleOfDoom.GameLogic.Services;
using TempleOfDoom.JsonGameParser;
using TempleOfDoom.UI;
using TempleOfDoom.UI.Services;

IInputReader consoleInputReader = new ConsoleInputReader();
IRenderer consoleRenderer = new ConsoleRenderer();
IGameParserStrategy gameParserStrategy = new JsonGameParserStrategy();

GameParser gameParser = new(new LocatableFactory(), gameParserStrategy);

bool isRunning = true;
consoleRenderer.Display("Welcone to Temple of Doom \r\nPress Enter to start \r\nPress Esc to quit");

while (isRunning)
{
    var key = consoleInputReader.GetNextInput();
    if (key == GameAction.Quit)
    {
        isRunning = false;
    }
    if (key == GameAction.Start)
    {
        GameLoop gameLoop = new(consoleRenderer, consoleInputReader, gameParser.Parse("Levels/TempleOfDoom_Extended_C_2223.json"));
        gameLoop.Run();
        consoleRenderer = new ConsoleRenderer();
        consoleRenderer.Display("Press Enter to start \r\nPress Esc to quit");
    }
}
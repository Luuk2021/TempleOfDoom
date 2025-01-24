using TempleOfDoom.GameLogic;
using TempleOfDoom.UI;
using TempleOfDoom.UI.Services;

ConsoleInputReader consoleInputReader = new();
ConsoleRenderer consoleRenderer = new();
GameLoop gameLoop = new(consoleRenderer, consoleInputReader);
gameLoop.Run();
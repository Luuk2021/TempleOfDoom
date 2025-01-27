using TempleOfDoom.GameLogic;
using TempleOfDoom.GameLogic.Services;
using TempleOfDoom.JsonGameParser;
using TempleOfDoom.UI;
using TempleOfDoom.UI.Services;

ConsoleInputReader consoleInputReader = new();
ConsoleRenderer consoleRenderer = new();
GameParser gameParser = new(new LocatableFactory());

GameLoop gameLoop = new(consoleRenderer, consoleInputReader, gameParser.Parse("Levels/TempleOfDoom.json"));
gameLoop.Run();
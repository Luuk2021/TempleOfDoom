using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.GameLogic.Models;
using TempleOfDoom.GameLogic.Services;

namespace TempleOfDoom.GameLogic
{
    public class GameLoop
    {
        private bool _isRunning = false;
        private IInputReader _inputReader;
        private Player _player;
        private List<Room> _rooms;
        private Room _currentRoom;
        private IRenderer _renderer;

        private Dictionary<GameAction, Action> _actions;

        public GameLoop(IRenderer renderer, IInputReader inputReader) {
            _renderer = renderer;
            _inputReader = inputReader;
            _rooms = [];

            BuildRooms(5, 7);

            _currentRoom = _rooms.First();

            _actions = new Dictionary<GameAction, Action>
            {
                { GameAction.None, () => {} },
                { GameAction.Quit, () => _isRunning = false },
                { GameAction.MoveUp, () => _player.TryMove(Direction.Up, _currentRoom.CanMoveTo) },
                { GameAction.MoveDown, () => _player.TryMove(Direction.Down, _currentRoom.CanMoveTo) },
                { GameAction.MoveLeft, () => _player.TryMove(Direction.Left, _currentRoom.CanMoveTo) },
                { GameAction.MoveRight, () => _player.TryMove(Direction.Right, _currentRoom.CanMoveTo) }
            };
        }
        public void BuildRooms(int width, int height)
        {
            var room = new Room(1);
            var player = new Player();
            player.Position = (1, 1);
            _player = player;
            room.Locatables.Add(player);

            for (int x = 0; x < width; x++)
            {
                var wall = new Wall();
                wall.Position = (x, 0);
                room.Locatables.Add(wall);

                wall = new Wall();
                wall.Position = (x, height - 1);
                room.Locatables.Add(wall);
            }

            for (int y = 0; y < height; y++)
            {
                var wall = new Wall();
                wall.Position = (0, y);
                room.Locatables.Add(wall);

                wall = new Wall();
                wall.Position = (width - 1, y);
                room.Locatables.Add(wall);
            }

            _rooms.Add(room);
        }
        public void Run()
        {
            _isRunning = true;
            _renderer.Display(_currentRoom);
            while (_isRunning)
            {
                var action = _inputReader.GetNextInput();
                _actions[action]();

                var playerCollisions = _currentRoom.CheckCollisions(_player);
                foreach (var collision in playerCollisions)
                {
                    _player.OnCollision(collision);
                    collision.OnCollision(_player);
                }
            }
        }
    }
}

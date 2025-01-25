using TempleOfDoom.GameLogic.Models;
using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;
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
        private List<ILocatable> _toRemove;

        private Dictionary<GameAction, Action> _actions;

        public GameLoop(IRenderer renderer, IInputReader inputReader) {
            _renderer = renderer;
            _inputReader = inputReader;
            _rooms = [];
            _toRemove = [];

            BuildRooms(10, 5);

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
            room.AddLocatable(player);

            for (int x = 0; x < width; x++)
            {
                var wall = new Wall();
                wall.Position = (x, 0);
                room.AddLocatable(wall);

                wall = new Wall();
                wall.Position = (x, height - 1);
                room.AddLocatable(wall);
            }

            for (int y = 0; y < height; y++)
            {
                var wall = new Wall();
                wall.Position = (0, y);
                room.AddLocatable(wall);

                wall = new Wall();
                wall.Position = (width - 1, y);
                room.AddLocatable(wall);
            }

            ICollidable boobyTrap = new Boobytrap(new BaseCollidable(2, 2), 1);
            room.AddLocatable(boobyTrap);

            ICollidable boobyTrap2 = new BaseCollidable(3, 3);
            boobyTrap2 = new DisappearingBoobyTrap(boobyTrap2, 1, ()=>_toRemove.Add(boobyTrap2));
            room.AddLocatable(boobyTrap2);

            _rooms.Add(room);
        }
        public void Run()
        {
            _isRunning = true;
            _renderer.Display(_currentRoom);

            HashSet<ICollidable> previousCollisions = new();

            while (_isRunning)
            {
                var action = _inputReader.GetNextInput();
                _actions[action]();

                var playerCollisions = _currentRoom.CheckCollisions(_player);
                var newCollisions = playerCollisions.Except(previousCollisions);
               
                foreach (var collision in newCollisions)
                {
                    _player.OnCollision(collision);
                    collision.OnCollision(_player);
                }
                previousCollisions = playerCollisions.ToHashSet();
                foreach (var locatable in _toRemove)
                {
                    _currentRoom.RemoveLocatable(locatable);
                    if (locatable is ICollidable collidable)
                    {
                        previousCollisions.Remove(collidable);
                    }
                }
                _toRemove.Clear();  
            }
        }
    }
}

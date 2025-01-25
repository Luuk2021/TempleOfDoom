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
        
        HashSet<ICollidable> _previousCollisions = [];
        private List<ILocatable> _toRemove;

        private Dictionary<GameAction, Action> _actions;

        public GameLoop(IRenderer renderer, IInputReader inputReader) {
            _renderer = renderer;
            _inputReader = inputReader;
            _rooms = [];
            _toRemove = [];

            BuildRooms(6, 5);

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
            var player = new Player(new BaseCollidable(1,1), 3);
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

            ICollidable sankaraStone = new BaseCollidable(4, 2);
            sankaraStone = new SankaraStone(sankaraStone, () => _toRemove.Add(sankaraStone));
            room.AddLocatable(sankaraStone);

            ICollidable sankaraStone2 = new BaseCollidable(4, 2);
            sankaraStone2 = new SankaraStone(sankaraStone2, () => _toRemove.Add(sankaraStone2));
            room.AddLocatable(sankaraStone2);

            ICollidable sankaraStone3 = new BaseCollidable(4, 2);
            sankaraStone3 = new SankaraStone(sankaraStone3, () => _toRemove.Add(sankaraStone3));
            room.AddLocatable(sankaraStone3);

            ICollidable sankaraStone4 = new BaseCollidable(4, 2);
            sankaraStone4 = new SankaraStone(sankaraStone4, () => _toRemove.Add(sankaraStone4));
            room.AddLocatable(sankaraStone4);

            ICollidable sankaraStone5 = new BaseCollidable(4, 2);
            sankaraStone5 = new SankaraStone(sankaraStone5, () => _toRemove.Add(sankaraStone5));
            room.AddLocatable(sankaraStone5);

            ICollidable sankaraStone6 = new BaseCollidable(4, 2);
            sankaraStone6 = new SankaraStone(sankaraStone6, () => _toRemove.Add(sankaraStone6));
            room.AddLocatable(sankaraStone6);

            ICollidable sankaraStone7 = new BaseCollidable(4, 3);
            sankaraStone7 = new SankaraStone(sankaraStone7, () => _toRemove.Add(sankaraStone7));
            room.AddLocatable(sankaraStone7);

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

                HandleCollisionsWithPlayer();
                HandleRemoves();
            }
        }
        private void HandleRemoves()
        {
            foreach (var locatable in _toRemove)
            {
                _currentRoom.RemoveLocatable(locatable);
                if (locatable is ICollidable collidable)
                {
                    _previousCollisions.Remove(collidable);
                }
            }
            _toRemove.Clear();
        }
        private void HandleCollisionsWithPlayer()
        {
            var playerCollisions = _currentRoom.CheckCollisions(_player);
            var newCollisions = playerCollisions.Except(_previousCollisions);

            foreach (var collision in newCollisions)
            {
                _player.OnCollision(collision);
                collision.OnCollision(_player);
            }
            _previousCollisions = playerCollisions.ToHashSet();

        }
  
    }
}

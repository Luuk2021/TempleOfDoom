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
        
        private List<ICollidable> _previousCollisions = [];
        private Dictionary<GameAction, Action> _actions;

        public GameLoop(IRenderer renderer, IInputReader inputReader) {
            _renderer = renderer;
            _inputReader = inputReader;
            _rooms = [];

            _player = new Player(new BaseCollidable(1, 1), 3);

            _rooms.Add(BuildRoom(1, 6, 5));
            _rooms.Add(BuildRoom(2, 10, 10));

            _currentRoom = _rooms.First();
            _currentRoom.AddLocatable(_player);

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
        public Room BuildRoom(int id, int width, int height)
        {
            var room = new Room(id);

            for (int x = 0; x < width; x++)
            {
                var wall = new Wall();
                wall.Position = (x, 0);
                room.AddLocatable(wall);

                if (x == width / 2)
                {
                    var door = new Door(new BaseCollidable(x, height - 1), 2, (1, 1));
                    room.AddLocatable(door);
                }
                else
                {
                    wall = new Wall();
                    wall.Position = (x, height - 1);
                    room.AddLocatable(wall);
                }
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
            boobyTrap2 = new DisappearingBoobyTrap(boobyTrap2, 1);
            room.AddLocatable(boobyTrap2);

            ICollidable sankaraStone = new BaseCollidable(4, 2);
            sankaraStone = new SankaraStone(sankaraStone);
            room.AddLocatable(sankaraStone);

            ICollidable sankaraStone2 = new BaseCollidable(4, 2);
            sankaraStone2 = new SankaraStone(sankaraStone2);
            room.AddLocatable(sankaraStone2);

            ICollidable sankaraStone3 = new BaseCollidable(4, 2);
            sankaraStone3 = new SankaraStone(sankaraStone3);
            room.AddLocatable(sankaraStone3);

            ICollidable sankaraStone4 = new BaseCollidable(4, 2);
            sankaraStone4 = new SankaraStone(sankaraStone4);
            room.AddLocatable(sankaraStone4);

            ICollidable sankaraStone5 = new BaseCollidable(4, 2);
            sankaraStone5 = new SankaraStone(sankaraStone5);
            room.AddLocatable(sankaraStone5);

            ICollidable sankaraStone6 = new BaseCollidable(4, 2);
            sankaraStone6 = new SankaraStone(sankaraStone6);
            room.AddLocatable(sankaraStone6);

            ICollidable sankaraStone7 = new BaseCollidable(4, 3);
            sankaraStone7 = new SankaraStone(sankaraStone7);
            room.AddLocatable(sankaraStone7);

            return room;
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
                HandleCollisionRemoves();
                HandleChangeRoom();
            }
        }

        private void HandleCollisionRemoves()
        {
            var toRemoves = _currentRoom.GetLocatables()
                .OfType<ICollidable>()
                .Where(c => { var d = GetDecorator<RemoveOnCollision>(c); return d != null && d.ToRemove; })
                .ToList();

            foreach (var toRemove in toRemoves)
            {
                _currentRoom.RemoveLocatable(toRemove);
                _previousCollisions.Remove(toRemove);
            }
        }

        private TDecorator? GetDecorator<TDecorator>(ICollidable collidable) where TDecorator : ICollidable
        {
            var current = collidable;

            while (current is CollidableDecorator decorator)
            {
                if (decorator is TDecorator targetDecorator)
                {
                    return targetDecorator;
                }
                current = decorator.Wrapee;
            }
            return default;
        }

        private void HandleChangeRoom()
        {
            var door = _currentRoom.GetLocatables()
                .OfType<ICollidable>()
                .Select(GetDecorator<Door>)
                .FirstOrDefault(d => d != null && d.GoToNextRoom);

            if (door == null) return;

            _currentRoom.RemoveLocatable(_player);
            _currentRoom = _rooms.First(r => r.Id == door.NextRoom);
            _currentRoom.AddLocatable(_player);
            _player.Position = door.NextRoomPlayerPosition;
            _renderer.Display(_currentRoom);
            door.GoToNextRoom = false;
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
            _previousCollisions = playerCollisions.ToList();
        }
    }
}

using CODE_TempleOfDoom_DownloadableContent;
using TempleOfDoom.GameLogic.Models;
using TempleOfDoom.GameLogic.Models.Adapters;
using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;
using TempleOfDoom.GameLogic.Services;

namespace TempleOfDoom.GameLogic
{
    public class GameLoop
    {
        private bool _isRunning = false;
        private IInputReader _inputReader;
        private Game _game;
        private Room _currentRoom;
        private IRenderer _renderer;
        
        private Dictionary<ICollidable, List<ICollidable>> _previousCollisions = [];
        private Dictionary<GameAction, Action> _actions;

        private int _stonesAmountToWin = 5;

        public GameLoop(IRenderer renderer, IInputReader inputReader, Game game) {
            _renderer = renderer;
            _inputReader = inputReader;
            _game = game;

            _currentRoom = _game.Rooms.First(r => r.GetLocatables().Any(l => l is Player));

            _actions = new Dictionary<GameAction, Action>
            {
                { GameAction.None, () => {} },
                { GameAction.Quit, () => _isRunning = false },
                { GameAction.MoveUp, () => _game.Player.TryMove(Direction.Up, _currentRoom.CanMoveTo) },
                { GameAction.MoveDown, () => _game.Player.TryMove(Direction.Down, _currentRoom.CanMoveTo) },
                { GameAction.MoveLeft, () => _game.Player.TryMove(Direction.Left, _currentRoom.CanMoveTo) },
                { GameAction.MoveRight, () => _game.Player.TryMove(Direction.Right, _currentRoom.CanMoveTo) },
                { GameAction.Shoot, () => Shoot() }
            };
        }

        private void Shoot()
        {
            var playerPosition = _game.Player.Position;

            var cardinalDirections = new (int x, int y)[]
            {
                (playerPosition.x, playerPosition.y - 1),
                (playerPosition.x, playerPosition.y + 1),
                (playerPosition.x - 1, playerPosition.y),
                (playerPosition.x + 1, playerPosition.y)
            };

            var damageables = _currentRoom.GetLocatables().OfType<IDamageable>();

            foreach (var damageable in damageables)
            {
                if (cardinalDirections.Any(dir => dir == damageable.Position))
                {
                    damageable.TakeDamage(1);
                }
            }
        }

        public void Run()
        {
            _isRunning = true;
            _renderer.Display(_currentRoom);

            while (_isRunning)
            {
                var action = _inputReader.GetNextInput();
                if (_actions.ContainsKey(action))
                {
                    _actions[action]();
                    if (action != GameAction.None)
                    {
                        HandleEnemyMovement();
                    }
                }

                HandleCollisions();
                HandleRemoves();
                HandleChangeRoom();
                CheckIfGameEnding();
            }
        }

        private void HandleEnemyMovement()
        {
            var enemies = _currentRoom.GetLocatables().OfType<EnemyAdapter>();
            foreach (var enemy in enemies)
            {
                enemy.TryMove(Direction.Up, _currentRoom.CanMoveTo);
            }
        }

        private void CheckIfGameEnding()
        {
            if (_game.Player.Health <= 0)
            {
                _isRunning = false;
                _renderer.DisplayGameOver();
            }
            if (_game.Player.GetItems().OfType<SankaraStone>().Count() == _stonesAmountToWin)
            {
                _isRunning = false;
                _renderer.DisplayWin();
            }
        }

        private void HandleRemoves()
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

            var toRemoveEnemeies = _currentRoom.GetLocatables()
                .OfType<EnemyAdapter>()
                .Where(e => e.Health <= 0)
                .ToList();

            foreach (var toRemove in toRemoveEnemeies)
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
                .OfType<IDoor>()
                .FirstOrDefault(d => d != null && d.GoToNextRoom);

            if (door == null) return;

            _currentRoom.RemoveLocatable(_game.Player);
            _currentRoom = _game.Rooms.First(r => r.Id == door.NextRoom);
            _currentRoom.AddLocatable(_game.Player);
            _game.Player.Position = door.NextRoomPlayerPosition;
            _renderer.Display(_currentRoom);
            door.GoToNextRoom = false;
        }

        private void HandleCollisions()
        {
            var collidables = _currentRoom.GetLocatables().OfType<ICollidable>();

            foreach (var collidable in collidables)
            {
                if (!_previousCollisions.ContainsKey(collidable))
                {
                    _previousCollisions[collidable] = new List<ICollidable>();
                }

                var currentCollisions = _currentRoom.CheckCollisions(collidable);
                var newCollisions = currentCollisions.Except(_previousCollisions[collidable]);
                var exitedCollisions = _previousCollisions[collidable].Except(currentCollisions);

                foreach (var collision in newCollisions)
                {
                    collidable.OnEnter(collision);
                }

                foreach (var collision in currentCollisions)
                {
                    collidable.OnStay(collision);
                }

                foreach (var collision in exitedCollisions)
                {
                    collidable.OnExit(collision);
                }

                _previousCollisions[collidable] = currentCollisions.ToList();
            }
        }
    }
}

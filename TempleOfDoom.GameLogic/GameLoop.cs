﻿using TempleOfDoom.GameLogic.Models;
using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Door;
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
        
        private List<ICollidable> _previousCollisions = [];
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
                { GameAction.MoveRight, () => _game.Player.TryMove(Direction.Right, _currentRoom.CanMoveTo) }
            };
        }

        public void Run()
        {
            _isRunning = true;
            _renderer.Display(_currentRoom);

            while (_isRunning)
            {
                var action = _inputReader.GetNextInput();
                if (_actions.ContainsKey(action)) _actions[action]();

                HandleCollisionsWithPlayer();
                HandleCollisionRemoves();
                HandleChangeRoom();
                CheckIfGameEnding();
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

        private void HandleCollisionsWithPlayer()
        {
            var playerCollisions = _currentRoom.CheckCollisions(_game.Player);
            var newCollisions = playerCollisions.Except(_previousCollisions);
            var exitedCollisions = _previousCollisions.Except(playerCollisions);

            foreach (var collision in newCollisions)
            {
                _game.Player.OnEnter(collision);
                collision.OnEnter(_game.Player);
            }

            foreach (var collision in playerCollisions)
            {
                _game.Player.OnStay(collision);
                collision.OnStay(_game.Player);
            }

            foreach (var collision in exitedCollisions)
            {
                _game.Player.OnExit(collision);
                collision.OnExit(_game.Player);
            }
            _previousCollisions = playerCollisions.ToList();
        }
    }
}

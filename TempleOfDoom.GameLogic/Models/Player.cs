using TempleOfDoom.GameLogic.Models.Interfaces;
using TempleOfDoom.GameLogic.Services;

namespace TempleOfDoom.GameLogic.Models
{
    public class Player : IWalkable, ICollidable, IDamageable
    {
        private readonly int _speed = 1;
        private int _health = 3;

        private Observable<int> _healthObservable = new();
        private Observable<((int x, int y) oldPos, ILocatable)> _positionObservable = new();
        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                _healthObservable.Notify(_health);
            }
        }
        private (int x, int y) _position;
        public (int x, int y) Position
        {
            get => _position;
            set
            {
                var old_position = _position;
                _position = value;
                _positionObservable.Notify((old_position, this));
            }
        }

        public Action<ICollidable> OnCollision => c => {
        };

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void TryMove(Direction direction, Func<(int x, int y), bool> canMoveTo)
        {
            var newPos = direction switch
            {
                Direction.Up => (Position.x, Position.y - _speed),
                Direction.Down => (Position.x, Position.y + _speed),
                Direction.Left => (Position.x - _speed, Position.y),
                Direction.Right => (Position.x + _speed, Position.y),
                _ => Position
            };

            if (canMoveTo(newPos))
            {
                Position = newPos;
            }
        }
        
        public IDisposable Subscribe(IObserver<int> observer)
        {
            return _healthObservable.Subscribe(observer);
        }

        public IDisposable Subscribe(IObserver<((int x, int y) oldPos, ILocatable)> observer)
        {
            return _positionObservable.Subscribe(observer);
        }
    }
}

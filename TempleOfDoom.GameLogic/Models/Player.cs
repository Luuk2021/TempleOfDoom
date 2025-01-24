using System.Numerics;
using TempleOfDoom.GameLogic.Services;

namespace TempleOfDoom.GameLogic.Models
{
    public class Player : Observable<((int x, int y) oldPos, ILocatable)>, IWalkable, ICollidable
    {
        private readonly int _speed = 1;
        private (int x, int y) _position;
        public (int x, int y) Position
        {
            get => _position;
            set
            {
                var old_position = _position;
                _position = value;
                Notify((old_position, this));
            }
        }

        public Action<ICollidable> OnCollision => c => {
        };

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
    }
}

using System.Collections.ObjectModel;
using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;
using TempleOfDoom.GameLogic.Services;

namespace TempleOfDoom.GameLogic.Models
{
    public class Player : CollidableDecorator, IWalkable, IDamageable, IInventory
    {
        private readonly int _speed;
        private int _health;
        private List<IHoldable> _items;
        public (int x, int y) OldPosition { get; private set; }

        private Observable<int> _healthObservable;
        private Observable<((int x, int y) oldPos, ILocatable)> _positionObservable;
        private Observable<(bool isAdded, IHoldable)> _inventoryObservable;

        public Player(ICollidable collidable, int health, int speed = 1) : base(collidable)
        {
            _speed = speed;
            _health = health;
            _items = [];
            _healthObservable = new Observable<int>();
            _positionObservable = new Observable<((int x, int y) oldPos, ILocatable)>();
            _inventoryObservable = new Observable<(bool isAdded, IHoldable)>();
        }

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                _healthObservable.Notify(_health);
            }
        }

        public override (int x, int y) Position
        {
            get => base.Position;
            set
            {
                OldPosition = base.Position;
                base.Position = value;
                _positionObservable.Notify((OldPosition, this));
            }
        }

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

        public IDisposable Subscribe(IObserver<(bool isAdded, IHoldable)> observer)
        {
            return _inventoryObservable.Subscribe(observer);
        }

        public void AddItem(IHoldable item)
        {
            _items.Add(item);
            _inventoryObservable.Notify((true, item));
        }

        public void RemoveItem(IHoldable item)
        {
            _items.Remove(item);
            _inventoryObservable.Notify((false, item));
        }

        public IEnumerable<IHoldable> GetItems()
        {
            return new ReadOnlyCollection<IHoldable>(_items);
        }
    }
}

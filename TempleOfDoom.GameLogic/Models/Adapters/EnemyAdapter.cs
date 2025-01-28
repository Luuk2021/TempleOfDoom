﻿using CODE_TempleOfDoom_DownloadableContent;
using System;
using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;
using TempleOfDoom.GameLogic.Services;


namespace TempleOfDoom.GameLogic.Models.Adapters
{
    public abstract class EnemyAdapter : CollidableDecorator, IDamageable, IWalkable
    {
        private class FieldAdapter : IField
        {
            public bool CanEnter => true;

            public IPlacable Item { get; set; }

            public IField GetNeighbour(int direction)
            {
                return null;
            }
        }

        private readonly Enemy _adaptee;

        private Observable<int> _healthObservable;
        private Observable<((int x, int y) oldPos, ILocatable)> _positionObservable;

        public override Action<ICollidable> OnEnter
        {
            get => c =>
            {
                Wrapee.OnEnter(c);
                if (c is Player p)
                {
                    p.TakeDamage(1);
                }
            };
        }

        public EnemyAdapter(Enemy enemy) : base(new BaseCollidable((enemy.CurrentXLocation, enemy.CurrentYLocation)))
        {
            _adaptee = enemy;
            _adaptee.CurrentField = new FieldAdapter();
            _healthObservable = new Observable<int>();
            _positionObservable = new Observable<((int x, int y) oldPos, ILocatable)>();
        }

        public int Health => _adaptee.NumberOfLives;

        private (int x, int y) _oldPosition;
        public override (int x, int y) Position
        {
            get => base.Position;
            set
            {
                _oldPosition = base.Position;
                base.Position = value;
                _positionObservable.Notify((_oldPosition, this));
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

        public void TakeDamage(int damage)
        {
            _adaptee.DoDamage(damage);
            _adaptee.CurrentField = new FieldAdapter();
            _healthObservable.Notify(_adaptee.NumberOfLives);
        }

        public void TryMove(Direction direction, Func<(int x, int y), bool> canMoveTo)
        {
            _adaptee.Move();
            if (canMoveTo((_adaptee.CurrentXLocation, _adaptee.CurrentYLocation)))
            {
                _positionObservable.Notify(((Position.x, Position.y), this));
                Position = (_adaptee.CurrentXLocation, _adaptee.CurrentYLocation);
            }
            else
            {
                _adaptee.CurrentXLocation = Position.x;
                _adaptee.CurrentYLocation = Position.y;
            }
        }
    }
}

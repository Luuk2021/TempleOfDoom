﻿using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public abstract class CollidableDecorator : ICollidable
    {
        private ICollidable _wrapee;
        public ICollidable Wrapee => _wrapee;
        public virtual (int x, int y) Position { get => _wrapee.Position; set => _wrapee.Position = value; }
        public virtual Action<ICollidable> OnEnter { get => _wrapee.OnEnter; }
        public virtual Action<ICollidable> OnStay { get => _wrapee.OnStay; }
        public virtual Action<ICollidable> OnExit { get => _wrapee.OnExit; }
        public CollidableDecorator(ICollidable wrapee)
        {
            _wrapee = wrapee;
        }
    }
}
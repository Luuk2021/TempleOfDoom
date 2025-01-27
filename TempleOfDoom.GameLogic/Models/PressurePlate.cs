using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;
using TempleOfDoom.GameLogic.Services;

namespace TempleOfDoom.GameLogic.Models
{
    public class PressurePlate : CollidableDecorator, IObservable<PressurePlate>
    {
        public bool IsPressed { get; private set; }
        private Observable<PressurePlate> _observable;
        public override Action<ICollidable> OnEnter
        {
            get => c =>
            {
                Wrapee.OnEnter(c);
                if (c is IWalkable w)
                {
                    IsPressed = true;
                    _observable.Notify(this);
                }
            };
        }


        public override Action<ICollidable> OnExit
        {
            get => c =>
            {
                Wrapee.OnExit(c);
                if (c is IWalkable w)
                {
                    IsPressed = false;
                    _observable.Notify(this);
                }
            };
        }

        public PressurePlate(ICollidable wrapee) : base(wrapee)
        {
            IsPressed = false;
            _observable = new Observable<PressurePlate>();
        }

        public IDisposable Subscribe(IObserver<PressurePlate> observer)
        {
            return _observable.Subscribe(observer);
        }
    }
}

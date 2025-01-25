namespace TempleOfDoom.GameLogic.Services
{
    public class Observable<T> : IObservable<T>
    {
        private List<IObserver<T>> _observers = [];

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber(()=>_observers.Remove(observer));
        }

        public void Notify(T value)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(value);
            }
        }
        private class Unsubscriber : IDisposable
        {
            private Action _unsubscribe;
            public Unsubscriber(Action unsubscribe)
            {
                _unsubscribe = unsubscribe;
            }
            public void Dispose()
            {
                _unsubscribe();
            }
        }
    }
}

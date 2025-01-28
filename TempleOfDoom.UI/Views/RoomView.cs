using TempleOfDoom.GameLogic.Models;
using TempleOfDoom.GameLogic.Models.Interfaces;
using TempleOfDoom.UI.Services;
using TempleOfDoom.UI.Views.TileViews;

namespace TempleOfDoom.UI.Views
{
    public class RoomView : IObserver<((int x, int y) oldPos, ILocatable locatable)>, IObserver<(bool isAdded, ILocatable locatable)>
    {
        private Dictionary<(int x, int y), Dictionary<ILocatable, TileView>> _tileViews;
        private (int x, int y) _offset;
        private TileViewFactory _tileViewFactory;
        private Dictionary<ILocatable, IDisposable> _unsubscriptions;

        public RoomView(Room room, (int x, int y) offset)
        {
            _tileViewFactory = new();
            _tileViews = [];
            _unsubscriptions = [];
            _offset = offset;

            room.Subscribe(this);

            SetLocatables(room.GetLocatables());
        }

        public int Display()
        {
            foreach (var tileViewDictionary in _tileViews.Values)
            {
                tileViewDictionary.Values.OrderByDescending(t => t.Layer).First().Display();
            }
            return _tileViews.Keys.Select(pos => pos.y).Max();
        }

        private void SetLocatables(IEnumerable<ILocatable> locatables)
        {
            foreach (var locatable in locatables)
            {
                if (locatable is IObservable<((int x, int y) oldPos, ILocatable locatable)> observableLocatable)
                {
                    _unsubscriptions[locatable] = observableLocatable.Subscribe(this);
                }
                var locatableName = locatable.GetType().Name.ToLower();

                var tileView = _tileViewFactory.CreateTileView(locatableName, [locatable]);

                AddTileView(tileView, locatable);
            }
        }

        private void AddTileView(TileView tileView, ILocatable locatable)
        {
            tileView.ScreenPosition = (locatable.Position.x + _offset.x, locatable.Position.y + _offset.y);
            if (_tileViews.TryGetValue(locatable.Position, out var tileViewDictionary))
            {
                tileViewDictionary.Add(locatable, tileView);
            }
            else
            {
                _tileViews.Add(locatable.Position, new Dictionary<ILocatable, TileView> { { locatable, tileView } });
            }
        }

        public void OnNext(((int x, int y) oldPos, ILocatable locatable) value)
        {
            var oldPositionTiles = _tileViews[value.oldPos];
            var tileView = oldPositionTiles[value.locatable];
            oldPositionTiles.Remove(value.locatable);

            if (oldPositionTiles.Count == 0)
            {
                _tileViews.Remove(value.oldPos);
                Console.SetCursorPosition(value.oldPos.x + _offset.x, value.oldPos.y + _offset.y);
                Console.Write(' ');
            }
            else
            {
                oldPositionTiles.Values.OrderByDescending(t => t.Layer).First().Display();
            }

            AddTileView(tileView, value.locatable);
            _tileViews[value.locatable.Position].Values.OrderByDescending(t => t.Layer).First().Display();
        }

        public void OnNext((bool isAdded, ILocatable locatable) value)
        {
            if (value.isAdded)
            {
                HandleAddedLocatable(value.locatable);
            }
            else
            {
                HandleDeletedLocatable(value.locatable);
            }
        }

        private void HandleAddedLocatable(ILocatable locatable)
        {
            SetLocatables([locatable]);
            _tileViews[locatable.Position].Values.OrderByDescending(t => t.Layer).First().Display();
        }

        private void HandleDeletedLocatable(ILocatable locatable)
        {
            var positionTiles = _tileViews[locatable.Position];
            var tileView = positionTiles[locatable];
            positionTiles.Remove(locatable);

            if (locatable is IObservable<((int x, int y) oldPos, ILocatable locatable)> observableLocatable)
            {
                _unsubscriptions[locatable].Dispose();
                _unsubscriptions.Remove(locatable);
            }
            if (positionTiles.Count == 0)
            {
                _tileViews.Remove(locatable.Position);
                Console.SetCursorPosition(locatable.Position.x + _offset.x, locatable.Position.y + _offset.y);
                Console.Write(' ');
            }
            else
            {
                positionTiles.Values.OrderByDescending(t => t.Layer).First().Display();
            }
        }

        public void OnCompleted()
        {
            // We do nothing here
        }
        public void OnError(Exception error)
        {
            // We do noting here
        }


    }
}

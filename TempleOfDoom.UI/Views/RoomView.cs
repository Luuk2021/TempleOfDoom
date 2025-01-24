using TempleOfDoom.GameLogic.Models;
using TempleOfDoom.GameLogic.Services;
using TempleOfDoom.UI.Services;
using TempleOfDoom.UI.Views.TileViews;

namespace TempleOfDoom.UI.Views
{
    public class RoomView : IObserver<((int x, int y) oldPos, ILocatable locatable)>, IObserver<List<ILocatable>>
    {
        private Dictionary<(int x, int y), Dictionary<ILocatable, TileView>> _tileViews;
        private (int x, int y) _offset;
        private TileViewFactory _tileViewFactory;

        public RoomView(Room room, (int x, int y) offset)
        {
            _tileViewFactory = new();
            _tileViews = [];

            SetLocatables(room.Locatables);
        }

        public void Display()
        {
            foreach (var tileViewDictionary in _tileViews.Values)
            {
                tileViewDictionary.Values.OrderByDescending(t => t.Layer).First().Display();
            }
        }

        //private void SetWalls(Room room)
        //{
        //    for (int x = 0; x < room.Width + 2; x++)
        //    {
        //        var tileViewUpper = _tileViewFactory.GetTileView("wall");
        //        tileViewUpper.ScreenPosition = (x + _offset.x, _offset.y);
        //        _tileViews.Add(tileViewUpper);
        //        var tileViewLower = _tileViewFactory.GetTileView("wall");
        //        tileViewLower.ScreenPosition = (x + _offset.x, room.Height + _offset.y + 1);
        //        _tileViews.Add(tileViewLower);
        //    }
        //    for (int y = 0; y < room.Height; y++)
        //    {
        //        var tileViewLeft = _tileViewFactory.GetTileView("wall");
        //        tileViewLeft.ScreenPosition = (_offset.x, y + _offset.y + 1);
        //        _tileViews.Add(tileViewLeft);
        //        var tileViewRight = _tileViewFactory.GetTileView("wall");
        //        tileViewRight.ScreenPosition = (room.Width + _offset.x + 1, y + _offset.y + 1);
        //        _tileViews.Add(tileViewRight);
        //    }
        //}

        private void SetLocatables(IList<ILocatable> locatables)
        {
            foreach (var locatable in locatables)
            {
                if (locatable is Observable<((int x, int y) oldPos, ILocatable locatable)> observableLocatable)
                {
                    observableLocatable.Subscribe(this);
                }
                var locatableName = locatable.GetType().Name.ToLower();
                var tileView = _tileViewFactory.GetTileView(locatableName);

                AddTileView(tileView, locatable);
            }
        }

        private void AddTileView(TileView tileView, ILocatable locatable)
        {
            tileView.ScreenPosition = (locatable.Position.x + _offset.x, locatable.Position.y + _offset.y);
            if (_tileViews.TryGetValue(tileView.ScreenPosition, out var tileViewDictionary))
            {
                tileViewDictionary.Add(locatable, tileView);
            }
            else
            {
                _tileViews.Add(tileView.ScreenPosition, new Dictionary<ILocatable, TileView> { { locatable, tileView } });
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
        public void OnNext(List<ILocatable> value)
        {
            _tileViewFactory = new();
            SetLocatables(value);
            Display();
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

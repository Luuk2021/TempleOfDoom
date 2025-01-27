using TempleOfDoom.GameLogic.Models.Interfaces;
using TempleOfDoom.UI.Services;

namespace TempleOfDoom.UI.Views
{
    public class InventoryView : IObserver<(bool isAdded, IHoldable)>
    {
        private (int x, int y) _offset;
        private IInventory _inventory;
        private TileViewFactory _tileViewFactory;
        private int _maxDeltaOffsetX;
        private IDisposable _subscription;
        public InventoryView(IInventory inventory, (int x, int y) offset, int maxDeltaOffsetX)
        {
            _tileViewFactory = new();
            _offset = offset;
            _maxDeltaOffsetX = maxDeltaOffsetX;
            _inventory = inventory;
            _subscription = _inventory.Subscribe(this);
        }

        public void SetNewOffset((int x, int y) offset, int maxDeltaOffsetX)
        {
            _offset = offset;
            _maxDeltaOffsetX = maxDeltaOffsetX;
        }

        public int Display()
        {
            int deltaOffsetY = 0;
            int deltaOffsetX = 0;
            Console.SetCursorPosition(_offset.x, _offset.y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Inventory: ");
            deltaOffsetY++;

            DisplayInventoryBorder(deltaOffsetY);
            deltaOffsetY++;

            var items = _inventory.GetItems();
            foreach (var item in _inventory.GetItems())
            {
                var holdableName = item.GetType().Name.ToLower();
                var tileView = _tileViewFactory.GetTileView(holdableName, [item]);

                tileView.ScreenPosition = (deltaOffsetX + _offset.x, deltaOffsetY + _offset.y);
                tileView.Display();
                if (deltaOffsetX >= _maxDeltaOffsetX)
                {
                    deltaOffsetY++;
                    deltaOffsetX = 0;
                }
                else
                {
                    deltaOffsetX++;
                }
            }
            deltaOffsetY++;

            for (int i = deltaOffsetX; i <= _maxDeltaOffsetX && i != 0; i++)
            {
                Console.SetCursorPosition(_offset.x + i, _offset.y + deltaOffsetY);
                Console.Write(" ");

            }
            DisplayInventoryBorder(deltaOffsetY);
            deltaOffsetY++;

            return deltaOffsetY;
        }

        private void DisplayInventoryBorder(int deltaOffsetY)
        {
            for (int i = 0; i <= _maxDeltaOffsetX; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(_offset.x + i, _offset.y + deltaOffsetY);
                Console.Write("-");

            }
        }

        public void OnNext((bool isAdded, IHoldable) value)
        {
            Display();
        }
        public void OnCompleted()
        {
            // We do nothing here
        }

        public void OnError(Exception error)
        {
            // We do nothing here
        }


    }
}

using TempleOfDoom.GameLogic.Models;
using TempleOfDoom.GameLogic.Services;
using TempleOfDoom.UI.Views;

namespace TempleOfDoom.UI.Services
{
    public class ConsoleRenderer : IRenderer
    {
        private RoomView? _roomView;
        private HealthView? _healthView;
        private InventoryView? _inventoryView;
        public void Display(Room room)
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var offset = (x:3, y:0);

            _roomView = new RoomView(room, offset);
            offset.y += _roomView.Display();

            offset.y += 3;

            var players = room.GetLocatables().OfType<Player>();
            foreach (var player in players) {

                offset.y += DisplayHealth(player, offset);

                offset.y += DisplayInventory(player, room.Width, offset);
            }
        }

        private int DisplayInventory(Player player, int maxWidth, (int x, int y) offset)
        {
            if (_inventoryView != null)
            {
                _inventoryView.SetNewOffset(offset, maxWidth);
            }
            else
            {
                _inventoryView = new InventoryView(player, offset, maxWidth);
            }
            return _inventoryView.Display();
        }
        private int DisplayHealth(Player player, (int x,int y) offset)
        {
            if (_healthView != null)
            {
                _healthView.SetNewOffset(offset);
            }
            else
            {
                _healthView = new HealthView(player, offset);
            }
            return _healthView.Display();
        }
    }
}

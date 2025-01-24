using TempleOfDoom.GameLogic.Models;
using TempleOfDoom.GameLogic.Services;
using TempleOfDoom.UI.Views;

namespace TempleOfDoom.UI.Services
{
    public class ConsoleRenderer : IRenderer
    {
        private (int x, int y) _roomOffset = (0, 0);
        public void Display(Room room)
        {
            var roomView = new RoomView(room, _roomOffset);
            roomView.Display();
        }
    }
}

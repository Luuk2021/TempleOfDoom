using TempleOfDoom.GameLogic.Models;
using TempleOfDoom.GameLogic.Services;
using TempleOfDoom.UI.Views;

namespace TempleOfDoom.UI.Services
{
    public class ConsoleRenderer : IRenderer
    {
        public void Display(Room room)
        {
            Console.Clear();
            var offset = (x:3, y:0);

            var roomView = new RoomView(room, offset);
            offset.y += roomView.Display();

            offset.y += 3;

            var players = room.GetLocatables().OfType<Player>();
            foreach (var player in players) {
                var healthView = new HealthView(player, offset);
                offset.y += healthView.Display();
            }
        }
    }
}

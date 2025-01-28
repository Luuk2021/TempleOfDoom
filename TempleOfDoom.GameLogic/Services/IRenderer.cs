using TempleOfDoom.GameLogic.Models;

namespace TempleOfDoom.GameLogic.Services
{
    public interface IRenderer
    {
        void Display(string message);
        void Display(Room room);
        void DisplayWin();
        void DisplayGameOver();
    }
}

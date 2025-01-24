using TempleOfDoom.GameLogic.Models;

namespace TempleOfDoom.UI.Views.TileViews
{
    public abstract class TileView
    {
        protected abstract char GetSymbol();
        protected abstract ConsoleColor GetColor();
        public abstract int Layer { get; }
        public (int x, int y) ScreenPosition { get; set; }
        public void Display()
        {
            Console.SetCursorPosition(ScreenPosition.x, ScreenPosition.y);
            Console.ForegroundColor = GetColor();
            Console.Write(GetSymbol());
        }
    }
}

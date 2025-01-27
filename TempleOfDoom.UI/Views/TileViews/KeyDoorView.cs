using TempleOfDoom.GameLogic.Models;

namespace TempleOfDoom.UI.Views.TileViews
{
    public class KeyDoorView : TileView
    {
        private KeyDoor _model;
        private readonly Dictionary<int, ConsoleColor> _colors = new()
        {
            { 0, ConsoleColor.Red },
            { 1, ConsoleColor.Green },
            { 2, ConsoleColor.Blue },
            { 3, ConsoleColor.Yellow },
            { 4, ConsoleColor.Magenta },
            { 5, ConsoleColor.Cyan },
            { 6, ConsoleColor.DarkRed },
            { 7, ConsoleColor.DarkGreen },
            { 8, ConsoleColor.DarkBlue },
            { 9, ConsoleColor.DarkYellow },
            { 10, ConsoleColor.DarkMagenta },
            { 11, ConsoleColor.DarkCyan }
        };
        public KeyDoorView(KeyDoor model)
        {
            _model = model;
        }
        public override int Layer => 1;

        protected override char GetSymbol()
        {
            return _model.IsHorizontal ? '=' : '|';
        }
        protected override ConsoleColor GetColor()
        {
            return _colors[_model.KeyId % _colors.Count];
        }
    }
}

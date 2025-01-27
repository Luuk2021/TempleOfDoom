using TempleOfDoom.GameLogic.Models;

namespace TempleOfDoom.UI.Views.TileViews
{
    public class KeyView : TileView
    {
        private Key _model;
        private readonly Dictionary<string, ConsoleColor> _colors = new()
        {
            { "red", ConsoleColor.Red },
            { "green", ConsoleColor.Green },
            { "blue", ConsoleColor.Blue },
            { "yellow", ConsoleColor.Yellow }
        };
        public KeyView(Key model)
        {
            _model = model;
        }
        public override int Layer => 1;

        protected override char GetSymbol()
        {
            return 'K';
        }
        protected override ConsoleColor GetColor()
        {
            return _colors[_model.KeyId];
        }
    }
}

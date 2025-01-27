
using TempleOfDoom.GameLogic.Models.Door;

namespace TempleOfDoom.UI.Views.TileViews
{
    public class ColoredView : TileView
    {
        private Colored _model;
        private readonly Dictionary<string, ConsoleColor> _colors = new()
        {
            { "red", ConsoleColor.Red },
            { "green", ConsoleColor.Green },
            { "blue", ConsoleColor.Blue },
            { "yellow", ConsoleColor.Yellow }
        };
        public ColoredView(Colored model)
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
            return _colors[_model.KeyId];
        }
    }
}

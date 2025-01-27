using System.Text;
using TempleOfDoom.GameLogic.Models.Decorators.Door;

namespace TempleOfDoom.UI.Views.TileViews
{
    public class ToggleDoorView : TileView
    {
        public override int Layer => 1;

        protected override char GetSymbol()
        {
            return '┴';
        }
        protected override ConsoleColor GetColor()
        {
            return ConsoleColor.White;
        }
    }
}

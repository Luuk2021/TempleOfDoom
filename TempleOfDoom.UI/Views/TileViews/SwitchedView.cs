namespace TempleOfDoom.UI.Views.TileViews
{
    public class SwitchedView : TileView
    {
        public override int Layer => 50;

        protected override char GetSymbol()
        {
            return '~';
        }
        protected override ConsoleColor GetColor()
        {
            return ConsoleColor.Magenta;
        }
    }
}

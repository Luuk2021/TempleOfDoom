namespace TempleOfDoom.UI.Views.TileViews
{
    public class ClosingGate : TileView
    {
        public override int Layer => 1;

        protected override char GetSymbol()
        {
            return '\u2229';
        }
        protected override ConsoleColor GetColor()
        {
            return ConsoleColor.White;
        }
    }
}

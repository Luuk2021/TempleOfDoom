namespace TempleOfDoom.UI.Views.TileViews
{
    public class OddLivesDoorView : TileView
    {
        public override int Layer => 1;

        protected override char GetSymbol()
        {
            return '\u2665';
        }
        protected override ConsoleColor GetColor()
        {
            return ConsoleColor.White;
        }
    }
}

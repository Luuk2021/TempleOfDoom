namespace TempleOfDoom.UI.Views.TileViews
{
    public class VerticalView : TileView
    {
        public override int Layer => 50;

        protected override char GetSymbol()
        {
            return 'E';
        }
        protected override ConsoleColor GetColor()
        {
            return ConsoleColor.Cyan;
        }
    }
}

namespace TempleOfDoom.UI.Views.TileViews
{
    public class WallView : TileView
    {
        public override int Layer => 99;
        protected override char GetSymbol()
        {
            return '#';
        }
        protected override ConsoleColor GetColor()
        {
            return ConsoleColor.White;
        }
    }
}

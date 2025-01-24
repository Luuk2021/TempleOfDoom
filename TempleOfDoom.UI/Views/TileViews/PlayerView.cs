namespace TempleOfDoom.UI.Views.TileViews
{
    public class PlayerView : TileView
    {
        public override int Layer => 99;

        protected override char GetSymbol()
        {
            return 'X';
        }
        protected override ConsoleColor GetColor()
        {
            return ConsoleColor.Blue;
        }
    }
}

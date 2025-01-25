namespace TempleOfDoom.UI.Views.TileViews
{
    public class DisappearingBoobyTrapView : TileView
    {
        public override int Layer => 1;

        protected override char GetSymbol()
        {
            return '@';
        }
        protected override ConsoleColor GetColor()
        {
            return ConsoleColor.White;
        }
    }
}

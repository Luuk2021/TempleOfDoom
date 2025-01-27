namespace TempleOfDoom.UI.Views.TileViews
{
    public class BaseDoorView : TileView
    {
        public override int Layer => 1;

        protected override char GetSymbol()
        {
            return ' ';
        }
        protected override ConsoleColor GetColor()
        {
            return ConsoleColor.Black;
        }
    }
}

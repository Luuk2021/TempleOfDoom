using TempleOfDoom.GameLogic.Models.Decorators.Door;

namespace TempleOfDoom.UI.Views.TileViews
{
    public class OpenOnStonesInRoomView : TileView
    {
        private OpenOnStonesInRoom _model;
        public OpenOnStonesInRoomView(OpenOnStonesInRoom model)
        {
            _model = model;
        }
        public override int Layer => 1;

        protected override char GetSymbol()
        {
            return char.Parse(""+_model.StoneAmount);
        }
        protected override ConsoleColor GetColor()
        {
            return ConsoleColor.White;
        }
    }
}

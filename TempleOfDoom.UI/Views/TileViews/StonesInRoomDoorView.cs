using TempleOfDoom.GameLogic.Models.Decorators.Door;

namespace TempleOfDoom.UI.Views.TileViews
{
    public class StonesInRoomDoorView : TileView
    {
        private StonesInRoomDoor _model;
        public StonesInRoomDoorView(StonesInRoomDoor model)
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

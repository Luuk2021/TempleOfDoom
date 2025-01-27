using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Door
{
    public class OpenOnStonesInRoom : DoorDecorator
    {
        private Room _room;
        public int StoneAmount { get; private set; }

        public OpenOnStonesInRoom(IDoor wrapee, Room room, int stoneAmount) : base(wrapee)
        {
            _room = room;
            StoneAmount = stoneAmount;
        }
        public override bool IsOpen()
        {
            return base.IsOpen() && _room.GetLocatables().Count(h => h is SankaraStone) == StoneAmount;
        }
    }
}

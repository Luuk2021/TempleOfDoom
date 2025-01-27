using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators.Door
{
    public class OpenOnStonesInRoom : DoorDecorator
    {
        private Room _room;
        public int StoneAmount { get; private set; }
        public override bool IsOpen { get => ((IDoor)Wrapee).IsOpen && _room.GetLocatables().Count(h => h is SankaraStone) == StoneAmount; }

        public OpenOnStonesInRoom(IDoor wrapee, Room room, int stoneAmount) : base(wrapee)
        {
            _room = room;
            StoneAmount = stoneAmount;
        }
    }
}

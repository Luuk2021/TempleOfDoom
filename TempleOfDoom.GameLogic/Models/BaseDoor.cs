using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class BaseDoor : BaseCollidable, IDoor
    {
        public int NextRoom { get; private set; }
        public (int x, int y) NextRoomPlayerPosition { get; private set; }
        public bool GoToNextRoom { get; set; }
        public bool IsOpen { get => true; }
        public BaseDoor((int x, int y) position, int nextRoom, (int x, int y) nextRoomPlayerPosition) : base(position)
        {
            NextRoom = nextRoom;
            NextRoomPlayerPosition = nextRoomPlayerPosition;
            GoToNextRoom = false;
        }
    }
}

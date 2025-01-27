namespace TempleOfDoom.GameLogic.Models.Interfaces
{
    public interface IDoor : ICollidable
    {
        public int NextRoom { get; }
        public (int x, int y) NextRoomPlayerPosition { get; }
        public bool GoToNextRoom { get; set; }
        public bool IsOpen();
    }
}

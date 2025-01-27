namespace TempleOfDoom.GameLogic.Models
{
    public class Game
    {
        public IEnumerable<Room> Rooms { get; set; }
        public Player Player { get; set; }

        public Game(IEnumerable<Room> rooms, Player player)
        {
            Rooms = rooms;
            Player = player;
        }
    }
}

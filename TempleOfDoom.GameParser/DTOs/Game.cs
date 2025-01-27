namespace TempleOfDoom.JsonGameParser.DTOs
{
    public class Game
    {
        public Room[] rooms { get; set; }
        public Connection[] connections { get; set; }
        public Player player { get; set; }
    }
}

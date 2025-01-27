namespace TempleOfDoom.JsonGameParser.DTOs
{
    public class Connection
    {
        public int NORTH { get; set; }
        public int SOUTH { get; set; }
        public Door[] doors { get; set; }
        public int WEST { get; set; }
        public int EAST { get; set; }
    }

}

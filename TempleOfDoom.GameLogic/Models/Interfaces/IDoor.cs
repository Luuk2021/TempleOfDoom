namespace TempleOfDoom.GameLogic.Models.Interfaces
{
    public interface IDoor : ICollidable
    {
        int ToRoomId { get; set; }
    }
}

using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class Door : CollidableDecorator
    {
        public int NextRoom { get; private set; }
        public (int x, int y) NextRoomPlayerPosition { get; private set; }
        public bool GoToNextRoom { get; set; }

        public override Action<ICollidable> OnCollision
        {
            get => c =>
            {
                if (c is Player p)
                {
                    GoToNextRoom = true;
                }
            };
        }

        public Door(ICollidable collidable, int nextRoom, (int x, int y) nextRoomPlayerPosition) : base(collidable)
        {
            NextRoom = nextRoom;
            NextRoomPlayerPosition = nextRoomPlayerPosition;
            GoToNextRoom = false;
        }
    }
}

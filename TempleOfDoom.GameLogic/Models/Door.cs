using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class Door : CollidableDecorator
    {
        public int NextRoom { get; private set; }
        public (int x, int y) NextRoomPlayerPosition { get; private set; }
        public bool GoToNextRoom { get; set; }
        protected virtual bool IsOpen { get => true; }

        public override Action<ICollidable> OnCollision
        {
            get => c =>
            {
                if (c is Player p)
                {
                    if (IsOpen)
                    {
                        _wrapee.OnCollision(c);
                        GoToNextRoom = true;
                    } else
                    {
                        p.Position = p.OldPosition;
                    }
                }
            };
        }

        public Door(ICollidable wrapee, int nextRoom, (int x, int y) nextRoomPlayerPosition) : base(wrapee)
        {
            NextRoom = nextRoom;
            NextRoomPlayerPosition = nextRoomPlayerPosition;
            GoToNextRoom = false;
        }
    }
}

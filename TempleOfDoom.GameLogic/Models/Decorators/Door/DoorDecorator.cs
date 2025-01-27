using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators.Door
{
    public abstract class DoorDecorator : CollidableDecorator, IDoor
    {
        public virtual int NextRoom { get => ((IDoor)Wrapee).NextRoom; }
        public virtual (int x, int y) NextRoomPlayerPosition { get => ((IDoor)Wrapee).NextRoomPlayerPosition; }
        public virtual bool GoToNextRoom { get => ((IDoor)Wrapee).GoToNextRoom; set => ((IDoor)Wrapee).GoToNextRoom = value; }
        public virtual bool IsOpen { get => ((IDoor)Wrapee).IsOpen; }

        public override Action<ICollidable> OnEnter
        {
            get => c =>
            {
                Wrapee.OnEnter(c);
            };
        }

        public DoorDecorator(IDoor wrapee) : base(wrapee)
        {
        }
    }
}

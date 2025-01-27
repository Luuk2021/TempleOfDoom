using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Door
{
    public abstract class DoorDecorator : CollidableDecorator, IDoor
    {
        public virtual int NextRoom { get => ((IDoor)Wrapee).NextRoom; }
        public virtual (int x, int y) NextRoomPlayerPosition { get => ((IDoor)Wrapee).NextRoomPlayerPosition; }
        public virtual bool GoToNextRoom { get => ((IDoor)Wrapee).GoToNextRoom; set => ((IDoor)Wrapee).GoToNextRoom = value; }
        private (int x, int y) _oldPosition { get; set; }
        public override Action<ICollidable> OnEnter
        {
            get => c =>
            {
                base.OnEnter(c);
                if (c is Player p)
                {
                    if (IsOpen())
                    {
                        GoToNextRoom = true;
                    }
                    else
                    {
                        GoToNextRoom = false;
                        _oldPosition = p.OldPosition;
                    }
                }
            };
        }

        public override Action<ICollidable> OnStay
        {
            get => c =>
            {
                base.OnStay(c);
                if (c is Player p)
                {
                    if (!GoToNextRoom)
                    {
                        p.Position = _oldPosition;
                    }
                }
            };
        }

        public DoorDecorator(IDoor wrapee) : base(wrapee)
        {
        }
        public virtual bool IsOpen()
        {
            return ((IDoor)Wrapee).IsOpen();
        }
    }
}

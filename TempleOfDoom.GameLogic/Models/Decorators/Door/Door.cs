using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators.Door
{
    public class Door : CollidableDecorator, IDoor
    {
        public virtual int NextRoom { get => ((IDoor)Wrapee).NextRoom; }
        public virtual (int x, int y) NextRoomPlayerPosition { get => ((IDoor)Wrapee).NextRoomPlayerPosition; }
        public virtual bool GoToNextRoom { get => ((IDoor)Wrapee).GoToNextRoom; set => ((IDoor)Wrapee).GoToNextRoom = value; }
        public virtual bool IsOpen { get => ((IDoor)Wrapee).IsOpen; }
        public override Action<ICollidable> OnEnter
        {
            get => c =>
            {
                
                if (c is Player p)
                {
                    if (IsOpen) 
                    {
                        Wrapee.OnEnter(c);
                        GoToNextRoom = true;
                    }
                    else
                    {
                        p.Position = p.OldPosition;
                    }
                }
            };
        }

        public Door(IDoor wrapee) : base(wrapee)
        {
        }
    }
}

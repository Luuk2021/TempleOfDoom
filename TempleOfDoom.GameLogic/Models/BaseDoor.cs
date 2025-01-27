using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class BaseDoor : BaseCollidable , IDoor
    {
        public int NextRoom { get; }
        public (int x, int y) NextRoomPlayerPosition { get; }
        public bool GoToNextRoom { get; set; }

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
                        p.Position = p.OldPosition;
                    }
                }
            };
        }

        public BaseDoor((int x, int y) position, int nextRoom, (int x, int y) nextRoomPlayerPosition) : base(position)
        {
            NextRoom = nextRoom;
            NextRoomPlayerPosition = nextRoomPlayerPosition;
        }
        public bool IsOpen()
        {
            return true;
        }
    }
}

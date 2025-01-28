using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.GameLogic.Models.Door;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators.Door
{
    public class TogglePosition : DoorDecorator
    {
        (int x, int y) _nextRoomPlayerPosition;
        (int x, int y) _otherPosition;
        public override (int x, int y) NextRoomPlayerPosition => _nextRoomPlayerPosition;
        public override Action<ICollidable> OnEnter
        {
            get => c =>
            {
                if (c is Player p)
                {
                    var temp = _otherPosition;
                    _otherPosition = _nextRoomPlayerPosition;
                    _nextRoomPlayerPosition = temp;
                }
                base.OnEnter(c);
            };
        }
        public TogglePosition(IDoor wrapee, (int x, int y) other) : base(wrapee)
        {
            _nextRoomPlayerPosition = wrapee.NextRoomPlayerPosition;
            _otherPosition = other;
        }
    }
}

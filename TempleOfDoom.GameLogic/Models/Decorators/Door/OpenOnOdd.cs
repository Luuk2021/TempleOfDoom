using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Door
{
    public class OpenOnOdd : DoorDecorator
    {
        private Player _player;
        public override Action<ICollidable> OnEnter
        {
            get => c =>
            {
                if (c is Player p)
                {
                    _player = p;
                }
                base.OnEnter(c);
            };
        }
        public OpenOnOdd(IDoor wrapee) : base(wrapee)
        {
        }
        public override bool IsOpen()
        {
            return base.IsOpen() && _player.Health % 2 == 1;
        }
    }
}

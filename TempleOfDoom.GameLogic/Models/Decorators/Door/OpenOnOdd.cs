using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators.Door
{
    public class OpenOnOdd : DoorDecorator
    {
        private Player _player;
        public override bool IsOpen { get => ((IDoor)Wrapee).IsOpen && _player.Health % 2 == 1; }
        public override Action<ICollidable> OnEnter
        {
            get => c =>
            {
                if (c is Player p)
                {
                    _player = p;
                }
                Wrapee.OnEnter(c);
            };
        }
        public OpenOnOdd(IDoor wrapee) : base(wrapee)
        {
        }
    }
}

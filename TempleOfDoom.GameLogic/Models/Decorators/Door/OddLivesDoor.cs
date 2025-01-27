using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators.Door
{
    public class OddLivesDoor : Door
    {
        private Player _player;
        public override bool IsOpen { get => ((IDoor)Wrapee).IsOpen && _player.Health % 2 == 1; }
        public OddLivesDoor(IDoor wrapee, Player player) : base(wrapee)
        {
            _player = player;
        }
    }
}

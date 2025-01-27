using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators.Door
{
    public class ClosingGate : DoorDecorator
    {
        private IDoor _other;
        private bool _isOpen = true;
        public override bool IsOpen { get => ((IDoor)Wrapee).IsOpen && _isOpen && _other.IsOpen; }
        public override Action<ICollidable> OnEnter
        {
            get => c =>
            {
                Wrapee.OnEnter(c);
                if (GoToNextRoom)
                {
                    _isOpen = false;
                }
            };
        }
        public ClosingGate(IDoor wrapee, IDoor other) : base(wrapee)
        {
            _other = other;
        }
    }
}
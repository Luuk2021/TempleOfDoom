using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Door
{
    public class ClosingGate : DoorDecorator
    {
        private IDoor _other;
        private bool _isOpen;
        public override Action<ICollidable> OnEnter
        {
            get => c =>
            {
                base.OnEnter(c);
                if (GoToNextRoom)
                {
                    _isOpen = false;
                }
            };
        }
        public ClosingGate(IDoor wrapee, IDoor other) : base(wrapee)
        {
            _other = other;
            _isOpen = true;
        }
        public override bool IsOpen()
        {
            return base.IsOpen() && _isOpen;
        }
    }
}
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Door
{
    public class ClosingGate : DoorDecorator
    {
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
        public ClosingGate(IDoor wrapee) : base(wrapee)
        {
            _isOpen = true;
        }
        public override bool IsOpen()
        {
            return base.IsOpen() && _isOpen;
        }
    }
}
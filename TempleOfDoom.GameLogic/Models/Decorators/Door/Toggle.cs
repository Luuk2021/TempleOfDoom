using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Door
{
    public class Toggle : DoorDecorator, IObserver<PressurePlate>
    {
        private IEnumerable<PressurePlate> _pressurePlates;
        private bool _isOpen;
        public Toggle(IDoor wrapee, Room room) : base(wrapee)
        {
            _isOpen = false;
            _pressurePlates = room.GetLocatables().OfType<PressurePlate>();
            if (_pressurePlates.Count() == 0)
            {
                _isOpen = true;
            }
            foreach (var item in _pressurePlates)
            {
                item.Subscribe(this);
            }
        }

        public override bool IsOpen()
        {
            return base.IsOpen() && _isOpen;
        }

        public void OnCompleted()
        {
            // we do nothing here
        }

        public void OnError(Exception error)
        {
            // we do nothing here
        }

        public void OnNext(PressurePlate value)
        {
            if (value.IsPressed)
            {
                _isOpen = !_isOpen;
            }
        }
    }
}

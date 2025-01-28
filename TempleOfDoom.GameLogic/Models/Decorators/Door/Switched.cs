using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Door
{
    public class Switched : DoorDecorator, IObserver<PressurePlate>
    {
        private IEnumerable<PressurePlate> _pressurePlates;
        private bool _isOpen;
        public Switched(IDoor wrapee, Room room) : base(wrapee)
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
            if (_pressurePlates.All(p => p.IsPressed))
            {
                _isOpen = true;
            }
        }
    }
}

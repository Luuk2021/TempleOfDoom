using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators.Door
{
    public class ToggleDoor : Door, IObserver<PressurePlate>
    {
        private IEnumerable<PressurePlate> _pressurePlates;
        private bool _isOpen;
        public override bool IsOpen { get => ((IDoor)Wrapee).IsOpen && _isOpen; }
        public ToggleDoor(IDoor wrapee, IEnumerable<PressurePlate> pressurePlates) : base(wrapee)
        {
            _isOpen = false;
            _pressurePlates = pressurePlates;
            foreach (var item in _pressurePlates)
            {
                item.Subscribe(this);
            }
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

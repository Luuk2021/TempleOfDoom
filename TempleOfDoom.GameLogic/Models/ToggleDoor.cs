using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class ToggleDoor : Door, IObserver<PressurePlate>
    {
        private IEnumerable<PressurePlate> _pressurePlates;
        private bool _isOpen;
        protected override bool IsOpen { get => _isOpen; }
        public ToggleDoor(ICollidable wrapee, int nextRoom, (int x, int y) nextRoomPlayerPosition, bool initialOpened, IEnumerable<PressurePlate> pressurePlates) : base(wrapee, nextRoom, nextRoomPlayerPosition)
        {
            _isOpen = initialOpened;
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

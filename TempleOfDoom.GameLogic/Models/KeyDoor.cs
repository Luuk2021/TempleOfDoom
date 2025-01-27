using TempleOfDoom.GameLogic.Models.Decorators;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models
{
    public class KeyDoor : Door
    {
        private IInventory _inventory;
        public int KeyId { get; private set; }
        public bool IsHorizontal { get; private set; }
        protected override bool IsOpen { get => _inventory.GetItems().Any(h => h is Key key && key.KeyId == KeyId); }
        public KeyDoor(ICollidable wrapee, int nextRoom, (int x, int y) nextRoomPlayerPosition, bool isHorizontal, IInventory inventory, int keyId) : base(wrapee, nextRoom, nextRoomPlayerPosition)
        {
            _inventory = inventory;
            KeyId = keyId;
            IsHorizontal = isHorizontal;
        }
    }
}

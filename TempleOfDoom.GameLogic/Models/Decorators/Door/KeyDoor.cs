using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators.Door
{
    public class KeyDoor : Door
    {
        private IInventory _inventory;
        public string KeyId { get; private set; }
        public bool IsHorizontal { get; private set; }
        public override bool IsOpen { get => ((IDoor)Wrapee).IsOpen && _inventory.GetItems().Any(h => h is Key key && key.KeyId == KeyId); }
        public KeyDoor(IDoor wrapee, bool isHorizontal, IInventory inventory, string keyId) : base(wrapee)
        {
            _inventory = inventory;
            KeyId = keyId;
            IsHorizontal = isHorizontal;
        }
    }
}

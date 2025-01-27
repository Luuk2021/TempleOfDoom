using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Door
{
    public class Colored : DoorDecorator
    {
        private IInventory _inventory;
        public string KeyId { get; private set; }
        public bool IsHorizontal { get; private set; }
        public override Action<ICollidable> OnEnter
        {
            get => c =>
            {
                if (c is Player p)
                {
                    _inventory = p;
                }
                base.OnEnter(c);
            };
        }
        public Colored(IDoor wrapee, bool isHorizontal, string keyId) : base(wrapee)
        {
            KeyId = keyId;
            IsHorizontal = isHorizontal;
        }
        public override bool IsOpen()
        {
            return base.IsOpen() && _inventory.GetItems().Any(h => h is Key key && key.KeyId == KeyId);
        }
    }
}

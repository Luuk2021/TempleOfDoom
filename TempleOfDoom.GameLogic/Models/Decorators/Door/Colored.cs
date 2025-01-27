using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators.Door
{
    public class Colored : DoorDecorator
    {
        private IInventory _inventory;
        public string KeyId { get; private set; }
        public bool IsHorizontal { get; private set; }
        public override bool IsOpen { get => ((IDoor)Wrapee).IsOpen && _inventory.GetItems().Any(h => h is Key key && key.KeyId == KeyId); }
        public override Action<ICollidable> OnEnter
        {
            get => c =>
            {
                if (c is Player p)
                {
                    _inventory = p;
                }
                Wrapee.OnEnter(c);
            };
        }
        public Colored(IDoor wrapee, bool isHorizontal, string keyId) : base(wrapee)
        {
            KeyId = keyId;
            IsHorizontal = isHorizontal;
        }
    }
}

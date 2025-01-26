using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class RemoveOnCollision : CollidableDecorator
    {
        public bool ToRemove { get; private set; }
        public override Action<ICollidable> OnCollision
        {
            get => c => 
            {
                _wrapee.OnCollision(c);
                ToRemove = true;
            };
        }

        public RemoveOnCollision(ICollidable wrapee) : base(wrapee)
        {
            ToRemove = false;
        }
    }
}

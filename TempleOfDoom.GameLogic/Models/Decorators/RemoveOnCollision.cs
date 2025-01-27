using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class RemoveOnCollision : CollidableDecorator
    {
        public bool ToRemove { get; private set; }
        public override Action<ICollidable> OnEnter
        {
            get => c => 
            {
                _wrapee.OnEnter(c);
                ToRemove = true;
            };
        }

        public RemoveOnCollision(ICollidable wrapee) : base(wrapee)
        {
            ToRemove = false;
        }
    }
}

using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Models.Decorators
{
    public class RemoveCollidableDecorator : CollidableDecorator
    {
        private Action _removeThisObjectCallBack;
        public override Action<ICollidable> OnCollision { get => c =>
        {
            _wrapee.OnCollision(c);
            _removeThisObjectCallBack();
        };
        }
        public RemoveCollidableDecorator(ICollidable wrapee, Action removeThisObjectCallBack) : base(wrapee)
        {
            _removeThisObjectCallBack = removeThisObjectCallBack;
        }
    }
}

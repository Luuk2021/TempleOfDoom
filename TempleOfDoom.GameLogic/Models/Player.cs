using TempleOfDoom.GameLogic.Services;

namespace TempleOfDoom.GameLogic.Models
{
    public class Player : Observable<ILocatable>, IWalkable
    {
        private int _x;
        private int _y;

        public int X
        {
            get => _x;
            set
            {
                _x = value;
                Notify(this);
            }
        }
        public int Y
        {
            get => _y;
            set
            {
                _y = value;
                Notify(this);
            }
        }

        public void Move(Services.GameAction action)
        {
            if(action == Services.GameAction.MoveUp)
            {
                Y--;
            }
            else if (action == Services.GameAction.MoveDown)
            {
                Y++;
            }
            else if (action == Services.GameAction.MoveLeft)
            {
                X--;
            }
            else if (action == Services.GameAction.MoveRight)
            {
                X++;
            }
        }
    }
}

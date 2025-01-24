using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.GameLogic.Models
{
    public interface IWalkable : ILocatable
    {
        void Move(Services.GameAction action);
    }
}

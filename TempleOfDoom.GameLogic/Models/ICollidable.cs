using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.GameLogic.Models
{
    public interface ICollidable : ILocatable
    {
        Action<ICollidable> OnCollision { get; }
    }
}

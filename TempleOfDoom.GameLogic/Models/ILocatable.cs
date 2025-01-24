using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.GameLogic.Models
{
    public interface ILocatable
    {
        int X { get; set; }
        int Y { get; set; }
    }
}

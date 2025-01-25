using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.UI.Views.TileViews
{
    public class BoobyTrapView : TileView
    {
        public override int Layer => 1;

        protected override char GetSymbol()
        {
            return 'O';
        }
        protected override ConsoleColor GetColor()
        {
            return ConsoleColor.White;
        }
    }
}

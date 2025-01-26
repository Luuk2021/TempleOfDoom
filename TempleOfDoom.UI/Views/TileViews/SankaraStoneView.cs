﻿namespace TempleOfDoom.UI.Views.TileViews
{
    public class SankaraStoneView : TileView
    {
        public override int Layer => 2;

        protected override char GetSymbol()
        {
            return 'S';
        }
        protected override ConsoleColor GetColor()
        {
            return ConsoleColor.DarkRed;
        }
    }
}

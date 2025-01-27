﻿namespace TempleOfDoom.UI.Views.TileViews
{
    public class ToggleView : TileView
    {
        public override int Layer => 1;

        protected override char GetSymbol()
        {
            return '┴';
        }
        protected override ConsoleColor GetColor()
        {
            return ConsoleColor.White;
        }
    }
}

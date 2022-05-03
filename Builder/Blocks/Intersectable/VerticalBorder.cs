using ConsoleGame.Base;
using ConsoleGame.Base.Enums;
using System;
using static ConsoleGame.ScenePlayer.ScenePlayer;

namespace ConsoleGame.Builder.Blocks.Intersectable
{
    public class VerticalBorder : ItemOnField
    {
        public new static char View = Setup.VER_BORDER_CELL;

        public VerticalBorder(object[] dataObj)
            : base(GenerateLine((ValueTuple<int, int>)dataObj[0], (int)dataObj[1], (ItemDirection)dataObj[2], View, true)) 
        {
            Tag = Setup.V_BORD_STRING;
        }

        public VerticalBorder((int, int) startPoint, int length, ItemDirection dir)
            : base(GenerateLine(startPoint, length, dir, View, true))
        {
            Tag = Setup.V_BORD_STRING;
        }
    }
}

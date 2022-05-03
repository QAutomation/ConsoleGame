using ConsoleGame.Base;
using ConsoleGame.Base.Enums;
using System;
using static ConsoleGame.ScenePlayer.ScenePlayer;

namespace ConsoleGame.Builder.Blocks.Intersectable
{
    public class HorizontalBorder : ItemOnField
    {
        public new static char View = Setup.HOR_BORDER_CELL;

        public HorizontalBorder(object[] dataObj)
            : base(GenerateLine((ValueTuple<int, int>)dataObj[0], (int)dataObj[1], (ItemDirection)dataObj[2], View, true))
        {
            Tag = "HBorder";
        }
    }
}
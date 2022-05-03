using ConsoleGame.Base;
using ConsoleGame.Base.Enums;
using static ConsoleGame.ScenePlayer.ScenePlayer;

namespace ConsoleGame.Builder.Blocks.Intersectable
{
    public class Wall : ItemOnField
    {
        public static new char View = Setup.WALL_CELL;
        public Wall((int, int) startPoint, int length, ItemDirection dir) : base(GenerateLine(startPoint, length, dir, View, true))
        {
            Tag = Setup.WALL_STRING;
        }
    }
}

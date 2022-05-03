using ConsoleGame.Base;
using static ConsoleGame.ScenePlayer.ScenePlayer;

namespace ConsoleGame.Builder.Blocks.NonIntersect
{
    public class Target : ItemOnField
    {
        public static new char View = SettingsModel.defaultConf.TARGET_CELL;
        public Target((int, int) startPoint) : base(GenerateBlock(startPoint, View, true))
        {
            Tag = Setup.TARGET_STRING;
        }
    }
}

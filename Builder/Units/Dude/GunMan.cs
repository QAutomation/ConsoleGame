using ConsoleGame.Base;

namespace ConsoleGame.Builder.Units.Dude
{
    public class GunMan : ItemOnField
    {
        public GunMan((int, int) startPoint) : base(GenerateBlock(startPoint, SettingsModel.defaultConf.GUNMAN_CELL, true))
        {
            Tag = "Gunman";
        }
    }
}
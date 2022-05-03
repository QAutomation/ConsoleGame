using ConsoleGame.Base;
using ConsoleGame.Builder.Units.Dude;
using ConsoleGame.Builder.Blocks.NonIntersect;
using static ConsoleGame.ScenePlayer.ScenePlayer;
using static ConsoleGame.Builder.SettingsModel;

namespace ConsoleGame
{
    class Program
    {
        static readonly GameField field = new GameField((defaultConf.WIDTH, defaultConf.HEIGHT));
        static readonly Target target = defaultConf.TARGET;
        static readonly Player player = new Player((2,2));
       
        static readonly ItemOnField[] allGoodThings = new ItemOnField[] { player, target };
        static readonly ItemOnField[] evil = defaultConf.ENEMIES;

        static (ItemOnField[] GoodShit, ItemOnField[] BadShit) TheWholeShit => (allGoodThings, evil);

        public static void Main()
        {
            field.Append(TheWholeShit.BadShit);
            field.Append(TheWholeShit.GoodShit);
            Start(field);
        }
    }
}
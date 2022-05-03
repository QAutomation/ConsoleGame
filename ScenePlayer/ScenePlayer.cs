using ConsoleGame.Base;
using ConsoleGame.Builder;
using ConsoleGame.Builder.Units.Dude;
using ConsoleGame.Extensions;

namespace ConsoleGame.ScenePlayer
{
    public static class ScenePlayer
    {
        public static SettingsModel Setup = SettingsModel.defaultConf;
        public static InputController InputController;

        public static void Start(GameField field)
        {
            Redraw(field, 0);

            InputController = new InputController(field);

            OutputHelper.Init();

            GameField.Player.SetInputHandler(InputController);
            GameField.Player.SetTickHandler(InputController);

            foreach (Enemy e in GameField.GetEnemies())
            {
                e.SetInputHandler(InputController);
            }

            InputController.KeyPressed += (keyInfo, gField) =>
            {
                if (keyInfo.Key == Setup.QUIT_FLAG)
                    Stop();
            };

            InputController.Init();
        }

        public static int Redraw(GameField field, int frame)
        {
            (0, 0).SetPosition();
            frame++;
            field.ToOutput().WriteAll();
            return frame;
        }

        static void Stop()
        {
            TimeHelper.Stop();
        }
    }
}
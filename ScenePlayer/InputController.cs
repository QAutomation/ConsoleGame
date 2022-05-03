using ConsoleGame.Base;
using ConsoleGame.Extensions;
using System;
using static ConsoleGame.ScenePlayer.ScenePlayer;

namespace ConsoleGame.ScenePlayer
{
    public class InputController
    {
        public delegate void KeyPressedEventHandler(ConsoleKeyInfo keyInfo, GameField field);
        public delegate void TickEventHandler(GameField field);
        public event KeyPressedEventHandler KeyPressed;
        static int frame = 1;
        static GameField gameField;

        static ConsoleKeyInfo keyinfo;

        static ConsoleKey lastKey;
        bool exit = false;

        public event TickEventHandler Tick;

        public InputController(GameField field)
        {
            gameField = field;
        }

        public void Init()
        {
            (0, (int)Setup.DEFAULT_COLOR).SetBrush();

            TimeHelper.SetTimers(
                (e) => { KeyPressed?.Invoke(keyinfo, gameField); },
                (e) =>
                {
                    Redraw(gameField, frame);
                    frame++;
                    $" ::: frame : {frame} ::: last key : {lastKey} ::: speed delay : {Setup.DELAY}".SetTitle();
                    Tick?.Invoke(gameField);
                });

            TimeHelper.Start();

            while (!exit)
            {
                var tempKInfo = keyinfo;
                keyinfo = OutputHelper.Read();

                TimeHelper.Stop();
                if (keyinfo.Key == ConsoleKey.Escape)
                {
                    TimeHelper.Stop();
                    exit = true;
                }
                else if (keyinfo.Key == ConsoleKey.OemPlus)
                {
                    Setup.DELAY += 4;
                    TimeHelper.GetKeyPressTimer().Interval = Setup.DELAY;
                    keyinfo = tempKInfo;
                }
                else if (keyinfo.Key == ConsoleKey.OemMinus)
                {
                    Setup.DELAY = (Setup.DELAY-4)>1?Setup.DELAY-4: Setup.DELAY;
                    TimeHelper.GetKeyPressTimer().Interval = Setup.DELAY;
                    keyinfo = tempKInfo;
                }
                else
                {

                }
                TimeHelper.Start();
                lastKey = keyinfo.Key;
            }
        }
    }
}
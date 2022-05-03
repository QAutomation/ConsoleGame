using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using static ConsoleGame.ScenePlayer.ScenePlayer;

namespace ConsoleGame.Extensions
{
    public static class TimeHelper
    {
        private static IEnumerable<Timer> gameTimers;
        static internal void Stop()
        {
            foreach (Timer timer in gameTimers)
            {
                timer.Stop();
            }
        }

        internal static void SetTimers(Action<ElapsedEventArgs> keyPressInvokeTimerDelegate, Action<ElapsedEventArgs> outputTimerDelegate)
        {
            gameTimers = new [] { keyPressInvokeTimerDelegate.GetTimer(119), outputTimerDelegate.GetTimer() };
        }

        internal static void Start()
        {
            foreach (Timer timer in gameTimers)
            {
                timer.Start();
            }
        }

        static Timer GetTimer(this Action<ElapsedEventArgs> delegateFunc, double interval = 0, bool isEnabled = false)
        {
            var timer = interval == 0
                ? new Timer(Setup.DELAY)
                : new Timer(interval);
            timer.Enabled = isEnabled;
            timer.AutoReset = true;
            timer.Elapsed += (s, e) => delegateFunc(e);

            return timer;
        }

        internal static Timer GetKeyPressTimer() => gameTimers.FirstOrDefault();
        internal static Timer GetOutTimer() => gameTimers.ElementAt(1);
    }
}

using ConsoleGame.Builder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleGame.Extensions
{
    public static class OutputHelper
    {
        public static void WriteAll(this IEnumerable<string> strings)
        {
            foreach (var s in strings)
                Console.WriteLine(s);
        }

        public static void SetBrush(this (int bColor, int fColor) colors)
        {
            Console.BackgroundColor = (ConsoleColor)colors.bColor;
            Console.ForegroundColor = (ConsoleColor)colors.fColor;
        }

        internal static void Init()
        {
            Console.Clear();

            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.CursorVisible = false;
            Console.OutputEncoding = SettingsModel.defaultConf.ENCODING;
            Console.ForegroundColor = SettingsModel.defaultConf.DEFAULT_COLOR;
        }

        public static void SetPosition(this (int x, int y) pos) =>
            Console.SetCursorPosition(pos.x, pos.y);

        public static void Write(string str) =>
            Console.Write(str);

        internal static void SetTitle(this string title) =>
            Console.Title = title;

        internal static ConsoleKeyInfo Read() =>
            Console.ReadKey(true);
    }
}
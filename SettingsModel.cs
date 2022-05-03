using ConsoleGame.Builder.Units.Dude;
using ConsoleGame.Base;
using ConsoleGame.Builder.Blocks.NonIntersect;
using System;
using System.Text;
using System.Threading;

namespace ConsoleGame.Builder
{
    public class SettingsModel
    {
        public int HEIGHT { get; set; } = 30;
        public int WIDTH { get; set; } = 90;
        public char EMPTY_CELL { get; set; } = ' ';
        public char HOR_BORDER_CELL { get; set; } = '▬';
        public char VER_BORDER_CELL { get; set; } = 'ǂ';
        public char WALL_CELL { get; set; } = 'X';
        public char TARGET_CELL { get; set; } = 'Ѳ';
        public char PLAYER_CELL { get; set; } = 'Ѩ';
        public char GUNMAN_CELL { get; set; } = 'Ѽ';
        public ConsoleKey QUIT_FLAG { get; set; } = ConsoleKey.Escape;
        public int DELAY { get; set; } = 142;
        public ConsoleColor DEFAULT_COLOR { get; set; } = ConsoleColor.Blue;
        public Target TARGET => new Target((WIDTH - 5, 2));

        public ItemOnField[] ENEMIES => new ItemOnField[]
        {
                new Enemy((WIDTH - 6, HEIGHT - 3)),
                new Enemy((WIDTH - 4, HEIGHT - 2)),
                new Enemy((WIDTH - 3, HEIGHT - 5)),
                new GunMan((WIDTH / 2, HEIGHT / 2))
        };
        
        public Encoding ENCODING { get; set; } = Encoding.Unicode;
        public string V_BORD_STRING { get; internal set; } = "VBorder";
        public string WALL_STRING { get; internal set; } = "Wall";
        public string TARGET_STRING { get; internal set; } = "Target";

        public static SettingsModel defaultConf => new SettingsModel();
    }
}
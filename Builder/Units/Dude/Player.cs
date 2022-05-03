using ConsoleGame.Base;
using ConsoleGame.Base.Enums;
using ConsoleGame.Builder.Blocks.Enums;
using ConsoleGame.ScenePlayer;
using System;

namespace ConsoleGame.Builder.Units.Dude
{
    public class Player : ItemOnField
    {
        static (string, int) sprite;

        public Player():base(GenerateBlock(
            (2,2), ScenePlayer.ScenePlayer.Setup.PLAYER_CELL, true))

        {
            Tag = CellType.USER.ToString();
            Setup();
        }

        public Player((int, int) startPoint) : base(GenerateBlock(startPoint, 'Ȫ', true))
        {

            Tag = CellType.USER.ToString();

            Setup();
        }

        private void Setup()
        {
            var frames = "ȪȫȬȭȮȯȰȱ";
            Sprite = frames;

            ChangeFrame = 2;
        }

        public void SetInputHandler(InputController keyBoardHandler) =>
            keyBoardHandler.KeyPressed += KeyPressedHandler;

        public void SetTickHandler(InputController keyBoardHandler) =>
            keyBoardHandler.Tick += TickHandler;

        private void TickHandler(GameField field)
        {
            NextFrame(field);
        }

        private void KeyPressedHandler(ConsoleKeyInfo keyInfo, GameField field)
        {
            ItemDirection direction = ItemDirection.NONE;
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    direction = ItemDirection.LEFT;
                    break;
                case ConsoleKey.RightArrow:
                    direction = ItemDirection.RIGHT;
                    break;
                case ConsoleKey.UpArrow:
                    direction = ItemDirection.UP;
                    break;
                case ConsoleKey.DownArrow:
                    direction = ItemDirection.DOWN;
                    break;

            }
            Shift(direction, field);
        }
    }
}
using ConsoleGame.Base;
using ConsoleGame.Base.Enums;
using ConsoleGame.Builder.Blocks.Enums;
using ConsoleGame.ScenePlayer;
using System;
using System.Collections.Generic;
using static ConsoleGame.ScenePlayer.ScenePlayer;
namespace ConsoleGame.Builder.Units.Dude
{
    public class Enemy : ItemOnField
    {

        public Enemy((int, int) startPoint)
            : base(GenerateBlock(startPoint, Setup.EMPTY_CELL, true))
        {
            Tag = CellType.ENEMY.ToString();
            Sprite = "ɹɻɼɺɽɾʃɿʄ";
            ChangeFrame = 2;
        }

        public Enemy((int X, int Y) pos, string sprite, int drawEachFrameNum = 3)
            : base(GenerateBlock(pos, Setup.EMPTY_CELL, true))
        {
            Tag = CellType.ENEMY.ToString();
            Sprite = sprite;
            ChangeFrame = drawEachFrameNum;
        }

        

        public void SetInputHandler(InputController keyBoardHandler) =>
            keyBoardHandler.Tick += KeyPressedHandler;

        private void KeyPressedHandler(GameField f)
        {
            NextFrame(f);
            if (nearestMovements.Count != 0) Shift(nearestMovements.Pop(), f);
            else MoveToPlayer();
        }
    }
}
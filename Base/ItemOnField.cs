using System.Collections.Generic;
using System.Linq;
using ConsoleGame.Base.Enums;
using static ConsoleGame.ScenePlayer.ScenePlayer;

namespace ConsoleGame.Base
{
    public class ItemOnField
    {
        public string Sprite { get; internal set; }
        public string Tag { get; internal set; }
        public char View { get; internal set; }
        public List<(int, int)> Body { get; internal set; } = new List<(int, int)> { };
        public bool IsIntersectable { get; private set; }
        public (int, int) Position { get; internal set; }

        internal Stack<ItemDirection> nearestMovements = new Stack<ItemDirection>();


        internal int ChangeFrame { get; set; } = 0;
        int frame = 0;
        int spriteNum = 0;

        public ItemOnField(IEnumerable<(int, int)> blocks, char view, bool isIntersect)
        {
            IsIntersectable = isIntersect;
            Position = (0, 0);
            View = view;
            Body.AddRange(blocks);
        }

        public void SetSprite(string sprite, int changeFrame)
        {
            Sprite = sprite;
            ChangeFrame = changeFrame;
        }

        public void NextFrame(GameField f)
        {
            if (frame == ChangeFrame)
            {
                View = Sprite[spriteNum];
                spriteNum = spriteNum < Sprite.Length - 1? spriteNum + 1 : 0;
                f.Reset(GameField.Items.ToArray());
                frame = 0;
                //MoveToPlayer();
            }
            frame = frame < 100 ? frame + 1 : 0;

            
        }
        internal void Shift(ItemDirection direction, GameField field, int step = 1)
        {
            if (direction != ItemDirection.NONE)
            {
                if (CheckPlaceForMove(direction))
                {
                    Body = Body.Select(p =>
                        direction == ItemDirection.UP ? (p.Item1, p.Item2 - step)
                         : direction == ItemDirection.DOWN ? (p.Item1, p.Item2 + step)
                         : direction == ItemDirection.LEFT ? (p.Item1 - step, p.Item2)
                         : direction == ItemDirection.RIGHT ? (p.Item1 + step, p.Item2)
                         : (-step, -step)
                    ).ToList();

                    Position = (Body.First().Item1, Body.First().Item2);
                    field.Reset(GameField.Items.ToArray());
                }
            }
        }

        public void MoveToPlayer()
        {

            List<ItemDirection> directions = new List<ItemDirection>();
            (int x, int y) pPos = GameField.Player.Position;
            (int x, int y) myPos = Position;

            if (pPos.x > myPos.x) directions.Add(ItemDirection.RIGHT);
            else directions.Add(ItemDirection.LEFT);

            if (pPos.y > myPos.y) directions.Add(ItemDirection.DOWN);
            else directions.Add(ItemDirection.UP);

            nearestMovements = new Stack<ItemDirection>(directions);
        }

        private bool CheckPlaceForMove(ItemDirection direction)
            => Body.TrueForAll(bI =>
                Setup.EMPTY_CELL == (direction == ItemDirection.UP ? GameField.Matrix[bI.Item2 - 1][bI.Item1].V
                         : direction == ItemDirection.DOWN ? GameField.Matrix[bI.Item2 + 1][bI.Item1].V
                         : direction == ItemDirection.LEFT ? GameField.Matrix[bI.Item2][bI.Item1 - 1].V
                         : direction == ItemDirection.RIGHT ? GameField.Matrix[bI.Item2][bI.Item1 + 1].V
                         : ' '));

        public ItemOnField(ItemOnField figure)
        {
            View = figure.View;
            IsIntersectable = figure.IsIntersectable;
            Position = figure.Position;
            Body.AddRange(figure.Body);
        }

        public ItemOnField((int x, int y) blockPosition, char view, bool isIntersect = false)
        {
            IsIntersectable = isIntersect;
            Position = blockPosition;
            View = view;
            Body = new List<(int, int)> { blockPosition };
        }

        public static ItemOnField GenerateLine((int X, int Y) start, int len, ItemDirection direction, char view, bool isIntersect = false)
        {
            int shift = 0;
            IEnumerable<(int, int)> line =
                        Enumerable.Range(0, len - 1).Select(item =>
                            {
                                var itemAfterShift = new ItemOnField(Copy(start, shift, direction), view) { IsIntersectable = isIntersect };
                                shift++;
                                return itemAfterShift.Position;
                            });

            return new ItemOnField(line, view, isIntersect);
        }

        public void Append(ItemOnField part) => Body.AddRange(part.Body);

        public static ItemOnField GenerateBlock((int x, int y) startPoint, char view, bool isIntersect)
            => new ItemOnField(startPoint, view) { IsIntersectable = isIntersect };

        public static (int, int) Copy((int X, int Y) origPoint, int shift, ItemDirection direction)
            => direction == ItemDirection.DOWN
                ? (origPoint.X, origPoint.Y + shift)
                : direction == ItemDirection.RIGHT
                    ? (origPoint.X + shift, origPoint.Y)
                    : (origPoint.X, origPoint.Y);
    }
}
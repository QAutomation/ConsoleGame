using ConsoleGame.Base.Enums;
using ConsoleGame.Builder.Blocks.Enums;
using ConsoleGame.Builder.Blocks.Intersectable;
using ConsoleGame.Builder.Units.Dude;
using System;
using System.Collections.Generic;
using System.Linq;
using static ConsoleGame.ScenePlayer.ScenePlayer;

namespace ConsoleGame.Base
{
    public class GameField : ItemOnField
    {
        static readonly (IEnumerable<int> yRange, IEnumerable<int> xRange) ranges =
            (Enumerable.Range(0, Setup.HEIGHT), Enumerable.Range(0, Setup.WIDTH));

        public static (int, int, char V)[][] Matrix { get; private set; } =
            ranges.yRange.Select(
                    rowNum => ranges.xRange.Select(
                        colNum => (colNum, rowNum, Setup.EMPTY_CELL)).ToArray()).ToArray();



        public (int width, int height) Size { get; private set; }

        public static List<ItemOnField> Items { get; internal set; } = new List<ItemOnField>();

        public GameField((int width, int height) size) : base((0, 0), Setup.EMPTY_CELL, false)
        {
            Size = size;
            var body = new List<(int, int)>();
            Body = body;
            SetBorders();

        }

        public static Player Player => (Player)Items.First(t => t.Tag == CellType.USER.ToString());
        internal static IEnumerable<Enemy> GetEnemies() => Items.Where(t => t.Tag == CellType.ENEMY.ToString()).Select(e => (Enemy)e);
        public void Append(ItemOnField[] currItems) => AddToMatrix(currItems);

        static ItemOnField GetHorizBorders()
            => GetBorders(
                new[] { (1, 0), (1, Setup.HEIGHT - 1) },
                Setup.WIDTH - 1, ItemDirection.RIGHT,
                (dataObj) => new HorizontalBorder(dataObj));

        static ItemOnField GetVertBorders()
            => GetBorders(
                    new[] { (0, 0), (Setup.WIDTH - 1, 0) },
                    Setup.HEIGHT, ItemDirection.DOWN,
                    (dataObj) => new VerticalBorder(dataObj));

        static ItemOnField GetBorders((int, int)[] points, int length, ItemDirection direction, Func<object[], ItemOnField> function)
        {
            var figures = points.Select(coord =>
                function(new object[] { coord, length, direction }));

            var item = figures.First();
            item.Append(figures.Last());
            return item;
        }

        public void Reset(ItemOnField[] items)
        {
            Items.Clear();
            Matrix = ranges.yRange.Select(
                        rowNum => ranges.xRange.Select(
                        colNum => (colNum, rowNum, Setup.EMPTY_CELL)).ToArray()).ToArray();
            AddToMatrix(items);
        }

        void SetBorders() => AddToMatrix(new[] { GetHorizBorders(), GetVertBorders() });

        void AddToMatrix(ItemOnField[] items)
        {
            foreach (var thisItem in items)
            {
                thisItem.Body.ForEach(((int x, int y) place) =>
                    Matrix[place.y][place.x] = Matrix[place.y][place.x].V == Setup.EMPTY_CELL ?
                        (place.y, place.x, thisItem.View) :
                        throw new Exception("Cell is occupied"));
            }
            Items.AddRange(items);
        }

        public IEnumerable<string> ToOutput() =>
            ranges.yRange.Select(
                row => new string(Matrix[row].Select(
                    ((int, int, char) cell) => cell.Item3).ToArray()));
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    class PointEvaluator : IWellStateEvaluator
    {
        private readonly int _startWallPoint = 3;
        private readonly int _startNeightBourPoint = 5;
        private readonly int _scaleConst = 1000;

        public int Evaluate(WellState wellState)
        {
            var addedBrick = wellState.Bricks[wellState.Bricks.Count - 1];
            var points = CountPoints(wellState, addedBrick);
            var pointsScaled = points * _scaleConst;

            return (int)pointsScaled;
        }

        private double CountPoints(WellState wellState, BrickPosition brickPos)
        {
            //DFS
            List<PointCords> points = new List<PointCords>();
            Stack<KeyValuePair<int, int>> stack = new Stack<KeyValuePair<int, int>>();

            var brick = brickPos.Brick;
            bool[,] visited = new bool[brick.Height, brick.Width];

            int row = 0, col = 0;
            for (int i = 0; i < brick.Height; i++)
                for (int j = 0; j < brick.Width; j++)
                    if (brick.Body[i, j])
                    {
                        row = i;
                        col = j;
                        break;
                    }
            stack.Push(new KeyValuePair<int, int>(row, col));
            while (stack.Count > 0)
            {
                var pair = stack.Pop();
                int i = pair.Key;
                int j = pair.Value;

                if (i > 0)
                {
                    if (brick.Body[i - 1, j] && !visited[i - 1, j])
                    {
                        stack.Push(new KeyValuePair<int, int>(i - 1, j));
                        visited[i - 1, j] = true;
                    }
                }
                if (j > 0)
                {
                    if (brick.Body[i, j - 1] && !visited[i, j - 1])
                    {
                        stack.Push(new KeyValuePair<int, int>(i, j - 1));
                        visited[i, j - 1] = true;
                    }
                }
                if (j + 1 < brick.Width)
                {
                    if (brick.Body[i, j + 1] && !visited[i, j + 1])
                    {
                        stack.Push(new KeyValuePair<int, int>(i, j + 1));
                        visited[i, j + 1] = true;
                    }
                }
                if (i + 1 < brick.Height)
                {
                    if (brick.Body[i + 1, j] && !visited[i + 1, j])
                    {
                        stack.Push(new KeyValuePair<int, int>(i + 1, j));
                        visited[i + 1, j] = true;
                    }
                }
                visited[i, j] = true;

                var possibleNeighbours = GetPossibleNeighbours(i, j, brickPos.Brick);
                points.AddRange(VerifyPossiblePoints(i, j, possibleNeighbours, wellState, brickPos));
            }

            var eliminatedDuplicatedPoints = EliminateDuplicats(points);

            double p = eliminatedDuplicatedPoints.Sum(point => GetPoints(point.Point.Y, point.PointEn));

            return p;
        }

        private IEnumerable<PointCords> EliminateDuplicats(List<PointCords> list)
        {
            List<PointCords> result = new List<PointCords>();
            bool isInCollection = false;
            foreach (PointCords t in list)
            {
                foreach (PointCords t1 in result)
                {
                    if (t.Point.Y == t1.Point.Y && t.Point.X == t1.Point.X &&
                        t.PointEn == t1.PointEn)
                    {
                        isInCollection = true;
                    }
                }
                if (!isInCollection)
                {
                    result.Add(t);
                }
                isInCollection = false;
            }
            return result;
        }

        private List<SideEnum> GetPossibleNeighbours(int row, int column, Brick brick)
        {
            List<SideEnum> list = new List<SideEnum>();
            if (row == 0)
                list.Add(SideEnum.Up);
            else
                if (!brick.Body[row - 1, column]) list.Add(SideEnum.Up);
            if (row == brick.Height - 1)
                list.Add(SideEnum.Down);
            else
                if (!brick.Body[row + 1, column]) list.Add(SideEnum.Down);

            if (column == 0)
                list.Add(SideEnum.Left);
            else
                if (!brick.Body[row, column - 1]) list.Add(SideEnum.Left);

            if (column == brick.Width - 1)
                list.Add(SideEnum.Right);
            else
                if (!brick.Body[row, column + 1]) list.Add(SideEnum.Right);

            return list;
        }

        private List<PointCords> VerifyPossiblePoints(int row, int column, List<SideEnum> list, WellState wellState, BrickPosition brickPos)
        {
            List<PointCords> result = new List<PointCords>();
            var brick = brickPos.Brick;

            int rowY = brick.Height - 1 - row + brickPos.Y;
            int columnX = column + brickPos.X;

            foreach (var side in list)
            {
                switch (side)
                {
                    case SideEnum.Up:
                        {
                            if (rowY == wellState.Fill.Count - 1) break;
                            if (wellState.IsFilled(columnX, rowY + 1))
                            {
                                result.Add(new PointCords(columnX, rowY + 1, PointEnum.Neighbour, side));
                            }

                            break;
                        }
                    case SideEnum.Down:
                        {
                            if (rowY == 0)
                                result.Add(new PointCords(columnX, rowY - 1, PointEnum.Wall, side));
                            else if (wellState.IsFilled(columnX, rowY - 1))
                            {
                                result.Add(new PointCords(columnX, rowY - 1, PointEnum.Neighbour, side));
                            }
                            break;
                        }
                    case SideEnum.Left:
                        {
                            if (columnX == 0)
                                result.Add(new PointCords(columnX - 1, rowY, PointEnum.Wall, side));
                            else if (wellState.IsFilled(columnX - 1, rowY))
                            {
                                result.Add(new PointCords(columnX - 1, rowY, PointEnum.Neighbour, side));
                            }
                            break;
                        }
                    case SideEnum.Right:
                        {
                            if (columnX == wellState.Well.Width - 1)
                                result.Add(new PointCords(columnX + 1, rowY, PointEnum.Wall, side));
                            else if (wellState.IsFilled(columnX + 1, rowY))
                            {
                                result.Add(new PointCords(columnX + 1, rowY, PointEnum.Neighbour, side));
                            }
                            break;
                        }
                }
            }
            return result;
        }
        private double GetPoints(int row, PointEnum pointEnum)
        {
            switch (pointEnum)
            {
                case PointEnum.Neighbour:
                    {
                        if (row <= 0) return _startNeightBourPoint;
                        return ((double)1 / (double)row) * _startNeightBourPoint;
                    }
                case PointEnum.Wall:
                    {
                        if (row <= 0) return _startNeightBourPoint;
                        return ((double)1 / (double)row) * _startWallPoint;
                    }

            }
            throw new Exception();
        }

        public enum SideEnum
        {
            Left, Right, Up, Down
        }

        private enum PointEnum
        {
            Neighbour, Wall
        }

        class PointCords
        {
            public Point Point { get; private set; }
            public PointEnum PointEn { get; private set; }
            private SideEnum Side { get; set; }

            public PointCords(int x, int y, PointEnum type, SideEnum side)
            {
                PointEn = type;
                Point = new Point(x, y);
                Side = side;
            }
        }
    }
}

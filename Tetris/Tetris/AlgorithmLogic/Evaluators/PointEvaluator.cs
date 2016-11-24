using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    /// <summary>
    /// Algorythm of counting points for given WellState
    /// </summary>
    public class PointEvaluator : IWellStateEvaluator
    {
        public readonly int _startWallPoint = 3;
        public readonly int _startNeightBourPoint = 5;
        public readonly int _scaleConst = 1000;

        /// <summary>
        /// Starter for points counting
        /// </summary>
        /// <param name="wellState">Given WellState with added brick</param>
        /// <returns>Number of points as an int</returns>
        public int Evaluate(WellState wellState)
        {
            if (wellState.Bricks.Count == 0) return 0;
            var addedBrick = wellState.Bricks[wellState.Bricks.Count - 1];
            var points = CountPoints(wellState, addedBrick);
            var pointsScaled = points * _scaleConst;

            return (int)pointsScaled;
        }

        /// <summary>
        /// Returns points for given well and brick
        /// </summary>
        /// <param name="wellState">wellstate</param>
        /// <param name="brickPos">brick with its position in well</param>
        /// <returns>Returns points as double</returns>
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

        /// <summary>
        /// Eliminates duplicates within collection of PointCords
        /// </summary>
        /// <param name="list">List of points to verify</param>
        /// <returns>returns enumeration of PointCords</returns>
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

        /// <summary>
        /// Tests one tile of brick and returns its possible neighbours - other bricks/walls
        /// </summary>
        /// <param name="row">Row as an int</param>
        /// <param name="column">Column as an int</param>
        /// <param name="brick">Brick to test</param>
        /// <returns>List of SideEnum</returns>
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

        /// <summary>
        /// Verify possible points
        /// </summary>
        /// <param name="row">Row as an int</param>
        /// <param name="column">Column as an int</param>
        /// <param name="list">List of sides to test</param>
        /// <param name="wellState">WellState</param>
        /// <param name="brickPos">Tested Brick</param>
        /// <returns>Returns List of veryfied PointsCords</returns>
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

        /// <summary>
        /// Function with logic of points given for given row
        /// </summary>
        /// <param name="row">Row as an int</param>
        /// <param name="pointEnum">Type of Point</param>
        /// <returns>returns single point</returns>
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
                        if (row <= 0) return _startWallPoint;
                        return ((double)1 / (double)row) * _startWallPoint;
                    }

            }
            throw new Exception();
        }

        /// <summary>
        /// 4 sides of brick's tile
        /// </summary>
        private enum SideEnum
        {
            Left, Right, Up, Down
        }
        /// <summary>
        /// Type of points
        /// </summary>
        private enum PointEnum
        {
            Neighbour, Wall
        }

        /// <summary>
        /// Helper class
        /// </summary>
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

﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Evaluators
{
    class PointEvaluator : IWellStateEvaluator
    {
        private int _startWallPoint = 2;
        private int _startNeightBourPoint = 3;


        List<int> pointsGiven = new List<int>();


        public int Evaluate(WellState wellState)
        {
            var addedBrick = wellState.Bricks[wellState.Bricks.Count - 1];

            var points = CountPoints(wellState, addedBrick);

            pointsGiven.Add((int)points);
            return (int)points;
        }

        private double CountPoints(WellState wellState, BrickPosition brickPos)
        {
            //DFS
            List<PointCords> probablePoints = new List<PointCords>();
            Stack<KeyValuePair<int, int>> stack = new Stack<KeyValuePair<int, int>>();

            var brick = brickPos.Body;
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

                var possibleNeighbours = GetPossibleNeighbours(i, j, brickPos);


                probablePoints.AddRange(GetPossiblePoints(i, j, possibleNeighbours, wellState, brickPos));
            }

            var eliminatedDuplicated = probablePoints.Distinct().ToList();

            double p = eliminatedDuplicated.Sum(point => GetPoints(point.Point.Y, point.PointEn));

            return p;
        }

        private List<SideEnum> GetPossibleNeighbours(int row, int column, BrickPosition brickPos)
        {
            var brickBody = brickPos.Body;
            List<SideEnum> list = new List<SideEnum>();
            if (row == 0) list.Add(SideEnum.Up);
            if (row > 0) if (!brickBody.Body[row - 1, column]) list.Add(SideEnum.Up);
            if (column == 0) list.Add(SideEnum.Left);
            if (column > 0) if (!brickBody.Body[row, column - 1]) list.Add(SideEnum.Left);
            if (row == brickBody.Height - 1) list.Add(SideEnum.Down);
            if (row < brickBody.Height - 1) if (!brickBody.Body[brickBody.Height - 1, column]) list.Add(SideEnum.Down);
            if (column == brickBody.Width - 1) list.Add(SideEnum.Right);
            if (column > 0) if (!brickBody.Body[row, brickBody.Width - 1]) list.Add(SideEnum.Right);

            return list;
        }

        private List<PointCords> GetPossiblePoints(int row, int column, List<SideEnum> list, WellState wellState, BrickPosition brickPos)
        {
            double points = 0;
            List<PointCords> result = new List<PointCords>();

            int rowY = row + brickPos.Y;
            int columnX = column + brickPos.X;

            foreach (var side in list)
            {
                switch (side)
                {
                    case SideEnum.Up:
                        {
                            if (rowY == 0)
                            {
                                result.Add(new PointCords(columnX, rowY - 1, PointEnum.Wall));
                                //points += GetWallPoints(rowY);
                            }
                            else
                            {
                                if (wellState.Fill[rowY - 1][column])
                                {
                                    result.Add(new PointCords(columnX, rowY - 1, PointEnum.Neighbour));
                                }
                            }
                            break;
                        }
                    case SideEnum.Down:
                        {
                            if (rowY == wellState.Fill.Count - 1) break;
                            if (wellState.Fill[rowY + 1][columnX])
                            {
                                result.Add(new PointCords(columnX, rowY + 1, PointEnum.Neighbour));

                            }

                            break;
                        }
                    case SideEnum.Left:
                        {
                            if (columnX == 0)
                            {
                                result.Add(new PointCords(columnX - 1, rowY, PointEnum.Wall));
                            }
                            else
                            {
                                if (wellState.Fill[rowY][columnX - 1])
                                {
                                    result.Add(new PointCords(columnX - 1, rowY, PointEnum.Neighbour));
                                }
                            }
                            break;
                        }
                    case SideEnum.Right:
                        {
                            if (columnX == wellState.Well.Width - 1)
                            {
                                result.Add(new PointCords(columnX + 1, rowY, PointEnum.Wall));

                            }
                            else
                            {
                                if (wellState.Fill[rowY][columnX + 1])
                                {
                                    result.Add(new PointCords(columnX + 1, rowY, PointEnum.Neighbour));
                                }
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
                        if (row == 0) return _startNeightBourPoint;
                        return (1 / row) * _startNeightBourPoint;
                    }
                case PointEnum.Wall:
                    {
                        if (row == 0) return _startNeightBourPoint;
                        return (1 / row) * _startWallPoint;
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
            public Point Point { get; set; }
            public PointEnum PointEn { get; set; }

            public PointCords(int x, int y, PointEnum type)
            {
                PointEn = type;
                Point = new Point(x, y);
            }
        }

    }
}

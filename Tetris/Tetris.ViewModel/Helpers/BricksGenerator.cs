using System;
using System.Collections.Generic;
using Tetris.Model.Models;

namespace Tetris.ViewModel.Helpers
{
    class BricksGenerator
    {
        private readonly int _maxBricks;
        private readonly int _field;
        private readonly int _maxWidth;
        private readonly int _maxHeight;

        public BricksGenerator(int field, int maxWidth, int maxHeight, int maxBricks)
        {
            _field = field;
            _maxWidth = maxWidth;
            _maxHeight = maxHeight;
            _maxBricks = maxBricks;
        }


        public List<Brick> GenerateBricks()
        {
            List<Brick> collection = new List<Brick>();
            int bricksCollected = _maxBricks;

            while (bricksCollected > 0)
            {
                var potentialBrick = GenerateRandomBrick();

                bool isGood = ValidateBrick(potentialBrick, collection);

                if (isGood)
                {
                    collection.Add(potentialBrick);
                    bricksCollected--;
                }
            }

            return collection;
        }

        private bool ValidateBrick(Brick brick, List<Brick> collection)
        {
            if (!ValidateConsistency(brick)) return false;

            return ValidateSize(brick) && ValidateEqualitySameSizes(brick, collection);
        }

        private bool ValidateEqualitySameSizes(Brick brick, List<Brick> collection)
        {
            foreach (var cBrick in collection)
            {
                if (cBrick == brick) return false;

                if (brick == cBrick.Rotate(RotateEnum.Right180)) return false;
            }
            return true;
        }

        private bool ValidateSize(Brick brick)
        {
            int rowMax = int.MinValue;
            int colMax = int.MinValue;

            int rowMin = int.MaxValue;
            int colMin = int.MaxValue;

            for (int i = 0; i < brick.Height; i++)
            {
                for (int j = 0; j < brick.Width; j++)
                {
                    if (brick.Body[i, j] != 0)
                    {
                        if (rowMax < i) rowMax = i;
                        if (colMax < j) colMax = j;

                        if (rowMin > i) rowMin = i;
                        if (colMin > j) colMin = j;
                    }
                }
            }
            return (rowMax == brick.Height - 1 && colMax == brick.Width - 1 && rowMin == 0 && colMin == 0);
        }


        private bool ValidateConsistency(Brick brick)
        {
            //DFS
            Stack<KeyValuePair<int, int>> stack = new Stack<KeyValuePair<int, int>>();

            bool[,] visited = new bool[brick.Height, brick.Width];

            int row = 0, col = 0;
            int field = _field;


            for (int i = 0; i < brick.Height; i++)
            {
                for (int j = 0; j < brick.Width; j++)
                {
                    if (brick.Body[i, j] != 0)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
            }


            stack.Push(new KeyValuePair<int, int>(row, col));
            while (stack.Count > 0)
            {
                var pair = stack.Pop();
                int i = pair.Key;
                int j = pair.Value;

                if (i > 0)
                {
                    if (brick.Body[i - 1, j] != 0 && !visited[i - 1, j])
                    {
                        stack.Push(new KeyValuePair<int, int>(i - 1, j));
                        visited[i - 1, j] = true;
                    }
                }
                if (j > 0)
                {
                    if (brick.Body[i, j - 1] != 0 && !visited[i, j - 1])
                    {
                        stack.Push(new KeyValuePair<int, int>(i, j - 1));

                        visited[i, j - 1] = true;
                    }
                }
                if (j + 1 < brick.Width)
                {
                    if (brick.Body[i, j + 1] != 0 && !visited[i, j + 1])
                    {
                        stack.Push(new KeyValuePair<int, int>(i, j + 1));
                        visited[i, j + 1] = true;


                    }
                }
                if (i + 1 < brick.Height)
                {
                    if (brick.Body[i + 1, j] != 0 && !visited[i + 1, j])
                    {
                        stack.Push(new KeyValuePair<int, int>(i + 1, j));
                        visited[i + 1, j] = true;
                    }
                }
                visited[i, j] = true;
                field--;
            }
            return field == 0;

        }


        private Brick GenerateRandomBrick()
        {
            Random r = new Random();

            int[,] body = new int[_maxHeight, _maxWidth];

            int field = _field;


            while (field != 0)
            {
                int row = r.Next(_maxHeight);
                int col = r.Next(_maxWidth);
                while (body[row, col] != 0)
                {
                    row = r.Next(_maxHeight);
                    col = r.Next(_maxWidth);
                }
                body[row, col] = 1;
                field--;
            }


            return new Brick() { Body = body };

        }
    }
}

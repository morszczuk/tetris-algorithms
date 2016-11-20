using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Models;

namespace Tetris.Helpers
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


        public List<BrickType> GenerateBricks()
        {
            List<BrickType> collection = new List<BrickType>();
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

        private bool ValidateBrick(BrickType brick, List<BrickType> collection)
        {
            if (!ValidateConsistency(brick.Body())) return false;
            if (!ValidateSize(brick.Body())) return false;
            return collection.All(b => b != brick);
        }

        private bool ValidateEqualitySameSizes(BrickType brick, List<BrickType> collection)
        {
            foreach (var cBrick in collection)
            {
                if (cBrick == brick) return false;
            }
            return true;
        }

        private bool ValidateSize(BrickBody brick)
        {
            int rowMax = int.MinValue;
            int colMax = int.MinValue;

            int rowMin = int.MaxValue;
            int colMin = int.MaxValue;

            for (int i = 0; i < brick.Height; i++)
            {
                for (int j = 0; j < brick.Width; j++)
                {
                    if (brick.Body[i, j])
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


        private bool ValidateConsistency(BrickBody brick)
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
                    if (brick.Body[i, j])
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
                field--;
            }
            return field == 0;

        }


        private BrickType GenerateRandomBrick()
        {
            Random r = new Random();

            bool[,] body = new bool[_maxHeight, _maxWidth];

            int field = _field;


            while (field != 0)
            {
                int row = r.Next(_maxHeight);
                int col = r.Next(_maxWidth);
                while (body[row, col])
                {
                    row = r.Next(_maxHeight);
                    col = r.Next(_maxWidth);
                }
                body[row, col] = true;
                field--;
            }


            return new BrickType(body);

        }
    }
}

using System;
using System.Collections.Generic;
using Tetris.Models;

namespace Tetris.Helpers
{
    /// <summary>
    /// Classes generates collection of bricks for given field, max width, max height and number of bricks - parameters should be used with caution
    /// </summary>
    public class BricksGenerator
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

        /// <summary>
        /// Generates random bricks then validate and add to collection if validation returns true, doing such until number of bricks are reached
        /// </summary>
        /// <returns>List of bricks</returns>
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


        /// <summary>
        /// Brick validations runned here
        /// </summary>
        /// <param name="brick"> brick to test</param>
        /// <param name="collection"> collection </param>
        /// <returns>true if brick is properly generated</returns>
        private bool ValidateBrick(Brick brick, List<Brick> collection)
        {
            if (!ValidateConsistency(brick)) return false;

            if (!ValidateSize(brick)) return false;

            return ValidateEqualitySameSizes(brick, collection);
        }

        /// <summary>
        /// Validates equality of same sized bricks
        /// </summary>
        /// <param name="brick">tested brick</param>
        /// <param name="collection">rest of bricks</param>
        /// <returns>true if brick is not in collection</returns>
        private bool ValidateEqualitySameSizes(Brick brick, List<Brick> collection)
        {
            foreach (var cBrick in collection)
            {
                if (cBrick == brick) return false;

                if (brick == BrickType.Rotate90Right(BrickType.Rotate90Right(cBrick))) return false;
            }
            return true;
        }

        /// <summary>
        /// Validates size of bricks
        /// </summary>
        /// <param name="brick">Tested brick</param>
        /// <returns>true if is properly generated</returns>
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

        /// <summary>
        /// Validates consistency of brick using DFS
        /// </summary>
        /// <param name="brick">tested brick</param>
        /// <returns>true if consistent</returns>
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

        /// <summary>
        /// Generate random brick
        /// </summary>
        /// <returns>Generated brick</returns>
        private Brick GenerateRandomBrick()
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


            return new Brick(body);

        }
    }
}

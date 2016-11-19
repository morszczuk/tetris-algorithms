using System;
using System.Collections.Generic;
using System.Linq;
using Tetris.Models;

namespace Tetris.Builders
{
    class BrickBuilder
    {
        public static Brick Build(bool[,] body)
        {
            var rotations = new Dictionary<RotateEnum, bool[,]>();
            var rotationsBinary = new Dictionary<RotateEnum, int[]>();
            foreach (RotateEnum rotation in Enum.GetValues(typeof(RotateEnum)))
            {
                var converted = ToBinaryRepresentation(Rotate(body, rotation));
                if (rotationsBinary.All(e => !e.Value.SequenceEqual(converted)))
                {
                    rotationsBinary.Add(rotation, converted);
                }
            }
            var brick = new Brick(body, rotationsBinary);
            return brick;
        }

        private static bool[,] Rotate(bool[,] body, RotateEnum rotate)
        {
            switch (rotate)
            {
                case RotateEnum.Right90:
                    return Rotate90Right(body);
                case RotateEnum.Right180:
                    return Rotate90Right(Rotate90Right(body));
                case RotateEnum.Right270:
                    return Rotate90Right(Rotate90Right(Rotate90Right(body)));
                default:
                    return body;
            }
        }

        private static bool[,] Rotate90Right(bool[,] body)
        {
            var newBody = new bool[body.GetLength(0), body.GetLength(1)];
            for (var i = 0; i < body.GetLength(0); i++)
            {
                for (var j = 0; j < body.GetLength(1); j++)
                {
                    newBody[j, body.GetLength(0) - i - 1] = body[i, j];
                }
            }
            return newBody;
        }

        private static int[] ToBinaryRepresentation(bool[,] body)
        {
            var converted = new int[body.GetLength(0)];
            for (var i = 0; i < body.GetLength(0); i++)
            {
                converted[i] = 0;
                for (var j = 0; j < body.GetLength(1); ++j)
                    if (body[i, j]) converted[i] |= 1 << j;
            }
            return converted;
        }
    }
}

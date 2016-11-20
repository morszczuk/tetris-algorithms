using System;
using System.Linq;

namespace Tetris.Models
{
    public class Brick
    {
        public int Width => Body.GetLength(1);
        public int Height => Body.GetLength(0);

        public bool[,] Body { get; }
        public uint[] BinaryBody { get; }
        
        public int TilesCount { get; }
        public BrickType BrickType { get; }

        public Brick(bool[,] body, BrickType brickType = null)
        {
            Body = body;
            BrickType = brickType;
            BinaryBody = ConvertToBinaryRepresentation(body);
            TilesCount = 0;
            for (var i=0;i<body.GetLength(0);i++)
                for (var j = 0; j < body.GetLength(1); j++)
                    if (body[i, j]) TilesCount++;
        }

        private static uint[] ConvertToBinaryRepresentation(bool[,] body)
        {
            var converted = new uint[body.GetLength(0)];
            for (var i = 0; i < body.GetLength(0); i++)
            {
                converted[i] = 0;
                for (var j = 0; j < body.GetLength(1); ++j)
                    if (body[i, j]) converted[i] |= (uint)1 << j;
            }
            return converted;
        }

        public static bool operator ==(Brick b1, Brick b2)
        {
            if ((object)b1 == null && (object)b2 == null) return true;
            if ((object)b1 == null || (object)b2 == null) return false;
            return b1.BinaryBody.SequenceEqual(b2.BinaryBody);
        }

        public static bool operator !=(Brick b1, Brick b2)
        {
            return !(b1 == b2);
        }
    }
}

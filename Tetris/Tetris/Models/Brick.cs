using System;
using System.Linq;

namespace Tetris.Models
{
    /// <summary>
    /// Class that stores brick body representation
    /// after all rotations and transformations.
    /// Apart from the bool amtrix reprsesntation
    /// it has also binary representation (array of uint)
    /// </summary>
    [Serializable]
    public class Brick
    {
        /// <summary>
        /// Width of the brick representation
        /// </summary>
        public int Width => Body.GetLength(1);

        /// <summary>
        /// Height of the brick representation
        /// </summary>
        public int Height => Body.GetLength(0);

        public bool[,] Body { get; }
        /// <summary>
        /// Binary representation of the brick
        /// </summary>
        public uint[] BinaryBody { get; }
        
        /// <summary>
        /// Number of trues in Body
        /// </summary>
        public int TilesCount { get; }

        /// <summary>
        /// Reference to parent BrickType
        /// </summary>
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

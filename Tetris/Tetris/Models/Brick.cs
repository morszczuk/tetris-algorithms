using System.Collections.Generic;
using System.Linq;

namespace Tetris.Models
{
    public class Brick
    {
        public int TilesCount { get; }
        public IEnumerable<RotateEnum> AvailableRotations => _rotationsBinary.Keys;

        private readonly bool[,] _baseBody;
        private readonly Dictionary<RotateEnum, int[]> _rotationsBinary;

        public Brick(bool[,] body, Dictionary<RotateEnum, int[]> rotationsBinary)
        {
            _baseBody = body;
            _rotationsBinary = rotationsBinary;
            TilesCount = 0;
            for (var i=0;i<body.GetLength(0);i++)
                for (var j = 0; j < body.GetLength(1); j++)
                    if (body[i, j]) TilesCount++;
        }

        public int Width(RotateEnum rotation = RotateEnum.Right0)
        {
            if (rotation == RotateEnum.Right90 || rotation == RotateEnum.Right270)
                return _baseBody.GetLength(0);
            return _baseBody.GetLength(1);
        }

        public int Height(RotateEnum rotation = RotateEnum.Right0)
        {
            if (rotation == RotateEnum.Right90 || rotation == RotateEnum.Right270)
                return _baseBody.GetLength(1);
            return _baseBody.GetLength(0);
        }

        public bool[,] Body(RotateEnum rotation = RotateEnum.Right0)
        {
            var body = new bool[Height(rotation),Width(rotation)];
            for(var i=0; i < Height(rotation); i++)
                for (var j = 0; j < Height(rotation); j++)
                    body[i, j] = (BinaryBody(rotation)[i] & (1 << j - 1)) != 0;
            return body;
        }

        public int[] BinaryBody(RotateEnum rotation = RotateEnum.Right0)
        {
            return _rotationsBinary[rotation];
        }

        public static bool operator ==(Brick b1, Brick b2)
        {
            if ((object)b1 == null && (object)b2 == null) return true;
            if ((object)b1 == null || (object)b2 == null) return false;
            return b1._rotationsBinary.Any(e => e.Value.SequenceEqual(b2.BinaryBody()));
        }

        public static bool operator !=(Brick b1, Brick b2)
        {
            return !(b1 == b2);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris.Models
{
    public class BrickType
    {
        public int TilesCount { get; }
        public IEnumerable<RotateEnum> AvailableRotations => _rotations.Keys;
        public int DefaultCount { get; set; }
        public BrickBody DefaultBody => Body();
        
        private readonly Dictionary<RotateEnum, BrickBody> _rotations;

        public BrickType(bool[,] body)
        {
            DefaultCount = 0;
            TilesCount = CalculateTiles(body);
            var brickBody = new BrickBody(body);
            _rotations = new Dictionary<RotateEnum, BrickBody>(4);
            foreach (RotateEnum rotation in Enum.GetValues(typeof(RotateEnum)))
            {
                if(_rotations.All(e => e.Value != brickBody))
                    _rotations.Add(rotation, brickBody);
                brickBody = brickBody.RotateBody90Right();
            }
        }

        public BrickBody Body(RotateEnum rotation = RotateEnum.Right0)
        {
            return _rotations[rotation];
        }

        public static int CalculateTiles(bool[,] body)
        {
            var tilesCount = 0;
            for (var i = 0; i < body.GetLength(0); i++)
                for (var j = 0; j < body.GetLength(1); j++)
                    if (body[i, j]) tilesCount++;
            return tilesCount;
        }

        public static bool operator ==(BrickType b1, BrickType b2)
        {
            if ((object)b1 == null && (object)b2 == null) return true;
            if ((object)b1 == null || (object)b2 == null) return false;
            return b1._rotations.Any(e => e.Value == b2.Body());
        }

        public static bool operator !=(BrickType b1, BrickType b2)
        {
            return !(b1 == b2);
        }
    }
}

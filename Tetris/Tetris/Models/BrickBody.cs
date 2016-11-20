using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Models
{
    public class BrickBody
    {
        public bool[,] Body { get; }
        public int[] BodyBinary { get; }

        public int Width => Body.GetLength(1);
        public int Height => Body.GetLength(0);

        public BrickBody(bool[,] body)
        {
            Body = body;
            BodyBinary = ConvertToBinaryRepresentation(body);
        }

        private int[] ConvertToBinaryRepresentation(bool[,] body)
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

        public BrickBody RotateBody90Right()
        {
            var newBody = new bool[Width, Height];
            for (var i = 0; i < Height; i++)
                for (var j = 0; j < Width; j++)
                    newBody[j, Height - i - 1] = Body[i, j];
            return new BrickBody(newBody);
        }

        public static bool operator ==(BrickBody b1, BrickBody b2)
        {
            if ((object)b1 == null && (object)b2 == null) return true;
            if ((object)b1 == null || (object)b2 == null) return false;
            return b1.BodyBinary.SequenceEqual(b2.BodyBinary);
        }

        public static bool operator !=(BrickBody b1, BrickBody b2)
        {
            return !(b1 == b2);
        }
    }
}

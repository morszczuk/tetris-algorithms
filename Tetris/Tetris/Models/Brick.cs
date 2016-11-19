using System;

namespace Tetris.Models
{
    public class Brick
    {
        public int Width => Body.GetLength(1);

        public int Height => Body.GetLength(0);

        public bool[,] Body { get; }
        public int TilesCount { get; }

        public int Cardinality { get; set; }

        public Brick(bool[,] body)
        {
            Body = body;
            TilesCount = 0;
            Cardinality = 0;
            for (var i=0;i<body.GetLength(0);i++)
                for (var j = 0; j < body.GetLength(1); j++)
                    if (body[i, j]) TilesCount++;
        }

        public Brick Rotate(RotateEnum rotate)
        {
            switch (rotate)
            {
                case RotateEnum.Right0:
                    return new Brick(Body);
                case RotateEnum.Right90:
                    return Rotate90Right();
                case RotateEnum.Right180:
                    return Rotate90Right().Rotate90Right();
                case RotateEnum.Right270:
                    return Rotate90Right().Rotate90Right().Rotate90Right();
            }
            throw new Exception();
        }

        private Brick Rotate90Right()
        {
            bool[,] newBody = new bool[this.Width, this.Height];
            for (var i = 0; i < this.Height; i++)
            {
                for (var j = 0; j < this.Width; j++)
                {
                    newBody[j, this.Height - i - 1] = this.Body[i, j];
                }
            }

            return new Brick(newBody);
        }

        public static bool operator !=(Brick b1, Brick b2)
        {

            if ((object) b1 == null && (object) b2 == null) return false;       
            if ((object)b1 == null && (object)b2 != null) return true;
            if ((object)b1 != null && (object)b2 == null) return true;

            if (b1.Width != b2.Width || b1.Height != b2.Height) return true;

            for (int i = 0; i < b1.Height; i++)
            {
                for (int j = 0; j < b1.Width; j++)
                {
                    if (b1.Body[i, j] != b2.Body[i, j]) return true;
                }
            }
            return false;
        }

        public static bool operator ==(Brick b1, Brick b2)
        {
            return !(b1 != b2);
        }
    }
}

using System;

namespace Tetris.Models
{
    [Serializable]
    public class BrickPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Brick Brick { get; }

        public BrickPosition(Brick brick, int x, int y)
        {
            Brick = brick;
            X = x;
            Y = y;
        }
    }
}

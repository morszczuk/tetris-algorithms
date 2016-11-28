using System;

namespace Tetris.Models
{
    /// <summary>
    /// Brick with its position in well
    /// </summary>
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

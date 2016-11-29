using System;

namespace Tetris.Models
{
    /// <summary>
    /// Brick with its position in a well
    /// </summary>
    [Serializable]
    public class BrickPosition
    {
        /// <summary>
        /// X coordinate in a well
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate in a well
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Brick body
        /// </summary>
        public Brick Brick { get; }

        public BrickPosition(Brick brick, int x, int y)
        {
            Brick = brick;
            X = x;
            Y = y;
        }
    }
}

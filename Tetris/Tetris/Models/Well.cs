using System;

namespace Tetris.Models
{
    /// <summary>
    /// Representation of single well
    /// </summary>
    [Serializable]
    public class Well
    {
        public int Width { get; }

        public Well(int width)
        {
            Width = width;
        }
    }
}

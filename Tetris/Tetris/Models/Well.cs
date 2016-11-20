using System;

namespace Tetris.Models
{
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

using System.Collections.Generic;
using Tetris.Models;

namespace Tetris
{
    class FileInputResult
    {
        public int WellWidth  { get; set; }

        public int BricksNumber => Bricks.Count;

        public List<Brick> Bricks { get; set; }

        public FileInputResult(int wellWidth, List<Brick> bricks)
        {
            Bricks = bricks;
            WellWidth = wellWidth;
        }
    }
}

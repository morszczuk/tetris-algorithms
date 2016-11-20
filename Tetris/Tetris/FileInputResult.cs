using System.Collections.Generic;
using Tetris.Models;

namespace Tetris
{
    class FileInputResult
    {
        public int WellWidth  { get; set; }

        public int BricksNumber => Bricks.Count;

        public List<BrickType> Bricks { get; set; }

        public FileInputResult(int wellWidth, List<BrickType> bricks)
        {
            Bricks = bricks;
            WellWidth = wellWidth;
        }
    }
}

using System.Collections.Generic;
using Tetris.Models;

namespace Tetris
{
    class FileInputResult
    {
        public int WellWidth  { get; set; }

        public int BricksNumber => BrickTypes.Count;

        public List<BrickType> BrickTypes { get; set; }

        public FileInputResult(int wellWidth, List<BrickType> brickTypes)
        {
            BrickTypes = brickTypes;
            WellWidth = wellWidth;
        }
    }
}

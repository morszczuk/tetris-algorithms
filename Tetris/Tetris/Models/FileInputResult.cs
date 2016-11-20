using System.Collections.Generic;

namespace Tetris.Models
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

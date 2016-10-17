using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Helpers
{
    class MathHelper
    {
        public static void CountGridCellsNo(out int gridWidth, out int gridHeight, int bricksNo)
        {
            int gridDimension = (int)Math.Ceiling(Math.Sqrt(bricksNo));

            gridWidth = gridDimension;
            gridHeight = gridDimension;
            if (gridWidth * (gridHeight - 1) >= bricksNo) gridHeight -= 1;

        }
        
    }
}

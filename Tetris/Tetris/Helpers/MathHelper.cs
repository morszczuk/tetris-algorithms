namespace Tetris.Helpers
{
    class MathHelper
    {
        public static void CountGridCellsNo(out int gridWidth, out int gridHeight, int bricksNo)
        {
            //int gridDimension = (int)Math.Ceiling(Math.Sqrt(bricksNo));
            //gridWidth = gridDimension;
            //gridHeight = gridDimension;
            //if (gridWidth * (gridHeight - 1) >= bricksNo) gridHeight -= 1;

            gridWidth = 3;
            gridHeight = bricksNo/3;
            if (bricksNo%3 != 0) gridHeight++;
        }
        
    }
}

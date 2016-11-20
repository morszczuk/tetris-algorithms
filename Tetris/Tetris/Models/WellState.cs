using System.Collections.Generic;
using System.Linq;

namespace Tetris.Models
{
    public class WellState
    {
        public Well Well { get; }
        public BricksShelf BricksShelf { get; }
        public List<BrickPosition> Bricks { get; }
        public List<bool[]> Fill { get; }
        public int FullRows { get; private set; }
        public int TilesCount { get; private set; }

        public WellState(Well well, BricksShelf bricksShelf)
        {
            Well = well;
            BricksShelf = bricksShelf;
            Bricks = new List<BrickPosition>();
            Fill = new List<bool[]>(10);
            FullRows = 0;
        }

        public WellState(WellState wellState)
        {
            Well = wellState.Well;
            Bricks = new List<BrickPosition>(wellState.Bricks);
            Fill = new List<bool[]>(wellState.Fill.Count);
            for (var i = 0; i < wellState.Fill.Count; i++)
                Fill.Add((bool[])wellState.Fill[i].Clone());
            FullRows = wellState.FullRows;
            BricksShelf = new BricksShelf(wellState.BricksShelf);
        }

        public bool AddBrick(BrickType brick, int x, int y, RotateEnum rotation = RotateEnum.Right0)
        {
            var brickBody = brick.Body(rotation);

            if (IsIntersecting(brickBody, x, y)) return false;

            TilesCount += brick.TilesCount;
            Bricks.Add(new BrickPosition(brick, x, y, rotation));
            for (var n = 0; n < brickBody.Height; n++)
            {
                if (y + n >= Fill.Count) Fill.Add(new bool[Well.Width]);
                for (var m = 0; m < brickBody.Width; m++)
                {
                    // We have to insert the brick in the reverse order
                    if (brickBody.Body[brickBody.Height - n - 1, m]) Fill[y + n][x + m] = true;
                }
                // Set index of the last full row
                if (Fill[y + n].All(el => el)) FullRows++;
            }
            return true;
        }

        public bool IsIntersecting(BrickBody brickBody, int x, int y)
        {
            for (var n = 0; n < brickBody.Height; n++)
            {
                if (y + n >= Fill.Count) Fill.Add(new bool[Well.Width]);

                for (var m = 0; m < brickBody.Width; m++)
                {
                    if (x + m >= Well.Width || Fill[y + n][x + m] && brickBody.Body[brickBody.Height - n - 1, m])
                        return true;
                }
            }
            return false;
        }
    }

}

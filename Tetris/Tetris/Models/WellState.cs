using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris.Models
{
    [Serializable]
    public class WellState
    {
        public Well Well { get; }
        public BricksShelf BricksShelf { get; }
        public List<BrickPosition> Bricks { get; }
        public List<ulong> Fill { get; }
        public int FullRows { get; private set; }
        public int TilesCount { get; private set; }

        public int PercentageFilled
        {
            get
            {
                double per = (double) TilesCount/(double)(Fill.Count*Well.Width);
                return (int) (per*100);
            }
        }

        public WellState(Well well, BricksShelf bricksShelf)
        {
            Well = well;
            BricksShelf = bricksShelf;
            Bricks = new List<BrickPosition>();
            Fill = new List<ulong>(6);
            FullRows = 0;
        }

        public WellState(WellState wellState)
        {
            Well = wellState.Well;
            Bricks = new List<BrickPosition>(wellState.Bricks);
            Fill = new List<ulong>(wellState.Fill);
            FullRows = wellState.FullRows;
            BricksShelf = new BricksShelf(wellState.BricksShelf);
            TilesCount = wellState.TilesCount;
        }

        public bool IsFilled(int x, int y)
        {
            return (Fill[y] & ((uint)1 << x)) != 0;
        }

        public bool AddBrick(Brick brick, int x, int y)
        {
            if (IsIntersecting(brick, x, y)) return false;
            TilesCount += brick.TilesCount;
            Bricks.Add(new BrickPosition(brick, x, y));
            for (var i = 0; i < brick.Height; i++)
            {
                if (y + i >= Fill.Count) AddRow();
                var rowWithOffset = brick.BinaryBody[brick.Height - i - 1] << x;
                Fill[y + i] |= rowWithOffset;
                if (Fill[y + i] == ulong.MaxValue) FullRows = y + i;
            }
            return true;
        }

        public bool IsIntersecting(Brick brick, int x, int y)
        {
            if (x + brick.Width > Well.Width) return true;
            for (var i = 0; i < brick.Height; i++)
            {
                if (y + i >= Fill.Count) return false;
                var rowWithOffset = brick.BinaryBody[brick.Height - i - 1] << x;
                if ((Fill[y + i] & rowWithOffset) != 0)
                    return true;
            }
            return false;
        }

        private void AddRow()
        {
            Fill.Add(ulong.MaxValue << Well.Width);
        }
    }

}

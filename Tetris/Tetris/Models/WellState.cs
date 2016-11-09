using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Helpers;

namespace Tetris.Models
{
    public class WellState
    {
        public List<BrickPosition> Bricks { get; }
        public Well Well { get; }
        public List<bool[]> Fill { get; }
        public int FullRows { get; private set; }

        public WellState(Well well)
        {
            Well = well;
            Bricks = new List<BrickPosition>();
            Fill = new List<bool[]>(10);
            FullRows = 0;
        }

        public void AddBrick(Brick brick, int x, int y)
        {
            Bricks.Add(new BrickPosition(brick, x, y));
            for (var n = 0; n < brick.Height; n++) 
            {
                if(y + n >= Fill.Count) Fill.Add(new bool[Well.Width]);
                for (var m = 0; m < brick.Width; m++)
                {
                    Fill[y + n][x + m] = brick.Body[n, m];
                }
                // Set index of the last full row
                if (FullRows == y + n - 1 && Fill[y + n].All(el => el))
                {
                    FullRows++;
                }
            }
        }

    }
    
}

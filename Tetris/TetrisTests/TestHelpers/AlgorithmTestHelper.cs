using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Models;

namespace TetrisTests.TestHelpers
{
    class AlgorithmTestHelper
    {

        public static WellState CreateEmptyWellState(int width)
        {
            return new WellState(new Well(width), EmptyBrickShelf());
        }

        public static WellState CreateFullWellState(int width, int height)
        {
            var wellState = new WellState(new Well(width), EmptyBrickShelf());
            var brick = CreateRectangleBrick(width, 1);
            for (var y = 0; y < height; y++)
                wellState.AddBrick(brick, 0, y);
            return wellState;
        }

        public static WellState CreateWellStateWithOneBrick(int width, int height)
        {
            var wellState = new WellState(new Well(width), EmptyBrickShelf());
            var brick = CreateRectangleBrick(width, 1);
            wellState.AddBrick(brick, 0, 0);
            return wellState;
        }
        public static WellState CreateWellStateWithNBricks(int width, int height,int n)
        {
            var wellState = new WellState(new Well(width), EmptyBrickShelf());
            var brick = CreateRectangleBrick(width, 1);
            for (var y = 0; y < n; y++)
                wellState.AddBrick(brick, 0, y);
            return wellState;
        }
        public static BricksShelf EmptyBrickShelf()
        {
            return new BricksShelf(new List<BrickType>());
        }

        public static Brick CreateRectangleBrick(int w, int h)
        {
            var body = new bool[h,w];
            for(var y=0; y < h; y++)
                for (var x = 0; x < w; x++)
                    body[y,x] = true;
            return new Brick(body);
        }
       
    }
}

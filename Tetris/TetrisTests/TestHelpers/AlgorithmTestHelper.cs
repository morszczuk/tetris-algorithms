﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.AlgorithmLogic.Evaluators;
using Tetris.AlgorithmLogic.Positioners;
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

        public static BricksShelf CreateBricksShelfWithNRectangleBricks(int n)
        {
            var types = new List<BrickType>(n);
            for (var i = 0; i < n; i++)
                types.Add(new BrickType(CreateRectangleBrick(i+1, i+2).Body));
            return new BricksShelf(types);
        }

        public class MockEvaluator : IWellStateEvaluator
        {
            public int Evaluate(WellState wellState)
            {
                return wellState.Fill.Count;
            }
        }

        public class MockPositioner : IBrickPositioner
        {
            public IEnumerable<WellState> PlaceBrick(WellState wellState, Brick brick)
            {
                wellState.AddBrick(brick, 0, 0);
                return new List<WellState>() { wellState };
            }
        }

    }
}

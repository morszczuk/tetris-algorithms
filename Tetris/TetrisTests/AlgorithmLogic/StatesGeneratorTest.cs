using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.AlgorithmLogic;
using Tetris.AlgorithmLogic.Evaluators;
using Tetris.AlgorithmLogic.Positioners;
using Tetris.Models;
using TetrisTests.TestHelpers;

namespace TetrisTests.AlgorithmLogic
{
    [TestClass]
    public class StatesGeneratorTest
    {
        [TestMethod]
        public void Generate_ForStateWithoutBricks_ReturnsEmptyList()
        {
            IBrickPositioner positioner = new DummyPositioner();
            var generator = new StatesGenerator(positioner);
            var bricks = AlgorithmTestHelper.CreateBricksShelfWithNRectangleBricks(0);
            var state = new WellState(new Well(4), bricks);
            Assert.AreEqual(0, generator.Generate(state).Count);
        }

        [TestMethod]
        public void Generate_ForStateWith1Brick_ReturnsStateForEachRotation()
        {
            IBrickPositioner positioner = new DummyPositioner();
            var generator = new StatesGenerator(positioner);
            var bricks = AlgorithmTestHelper.CreateBricksShelfWithNRectangleBricks(1);
            var state = new WellState(new Well(4), bricks);
            Assert.AreEqual(2, generator.Generate(state).Count);
        }

        [TestMethod]
        public void Generate_ForStateWith3Brick_ReturnsStateForEachBrickAndRotation()
        {
            IBrickPositioner positioner = new DummyPositioner();
            var generator = new StatesGenerator(positioner);
            var bricks = AlgorithmTestHelper.CreateBricksShelfWithNRectangleBricks(3);
            var state = new WellState(new Well(4), bricks);
            Assert.AreEqual(6, generator.Generate(state).Count);
        }

        private class DummyPositioner : IBrickPositioner
        {
            public IEnumerable<WellState> PlaceBrick(WellState wellState, Brick brick)
            {
                return new List<WellState>() { wellState };
            }
        }
    }
}

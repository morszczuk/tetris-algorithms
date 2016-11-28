using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.AlgorithmLogic;
using Tetris.AlgorithmLogic.Positioners;
using Tetris.Models;

namespace TetrisTests.AlgorithmLogic
{
    [TestClass]
    public class StatesGeneratorTest
    {
        [TestMethod]
        public void Generate_ForStatesWithoutBricks_ReturnsEmptyList()
        {
            var generator = new StatesGenerator();

        }

        private class DummyPositioner : IBrickPositioner
        {
            public IEnumerable<WellState> PlaceBrick(WellState wellState, Brick brick)
            {
                return wellState;
            }
        }
    }
}

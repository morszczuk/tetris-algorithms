using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TetrisTests.TestHelpers;

namespace TetrisTests.Models
{
    [TestClass]
    public class WellStateTest
    {
        [TestMethod]
        public void IsFilled_ReturnsTrueOnFilledCoordinates()
        {
            var wellState = AlgorithmTestHelper.CreateFullWellState(4, 4);
            Assert.IsTrue(wellState.IsFilled(2,2));
        }

        [TestMethod]
        public void IsFilled_ReturnsFalseOnEmptyCoordinates()
        {
            var wellState = AlgorithmTestHelper.CreateFullWellState(4, 4);
            wellState.AddBrick(AlgorithmTestHelper.CreateRectangleBrick(2, 2), 0, 4);
            Assert.IsFalse(wellState.IsFilled(3, 5));
        }

        [TestMethod]
        public void AddBrick_WithOverlappingBricks_ReturnsFalse()
        {
            var wellState = AlgorithmTestHelper.CreateFullWellState(4, 4);
            var brick = AlgorithmTestHelper.CreateRectangleBrick(2, 2);
            Assert.IsFalse(wellState.AddBrick(brick, 2, 2));
        }

        [TestMethod]
        public void AddBrick_WitEmptySpace_ReturnsTrueAndAddsBrick()
        {
            var wellState = AlgorithmTestHelper.CreateEmptyWellState(4);
            var brick = AlgorithmTestHelper.CreateRectangleBrick(2, 2);
            Assert.IsTrue(wellState.AddBrick(brick, 0, 0));
            Assert.AreEqual(1, wellState.Bricks.Count);
        }
    }
}

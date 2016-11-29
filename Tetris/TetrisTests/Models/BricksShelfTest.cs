using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TetrisTests.TestHelpers;

namespace TetrisTests.Models
{
    [TestClass]
    public class BricksShelfTest
    {
        [TestMethod]
        public void AvailableBricks_WithNonZeroCountity_ReturnsAllBricks()
        {
            var shelf = AlgorithmTestHelper.CreateBricksShelfWithNRectangleBricks(3);
            Assert.AreEqual(3, shelf.AvailableBrickTypes.Count());
        }

        [TestMethod]
        public void AvailableBricks_WithZeroCountity_ReturnsBricksWithNonZeroCount()
        {
            var shelf = AlgorithmTestHelper.CreateBricksShelfWithNRectangleBricks(3);
            shelf.Bricks[shelf.Bricks.First().Key] = 0;
            Assert.AreEqual(2, shelf.AvailableBrickTypes.Count());
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.Helpers;

namespace TetrisTests.Helpers
{
    [TestClass]
    public class BricksGeneratorTest
    {
        [TestMethod]
        public void BricksGeneratorEmpty()
        {
            BricksGenerator generator = new BricksGenerator(10, 5, 5, 0);
            var list = generator.GenerateBricks();
            Assert.IsTrue(list.Count == 0);
        }
        [TestMethod]
        public void BricksGeneratorOneBrick()
        {
            BricksGenerator generator = new BricksGenerator(10, 5, 5, 1);
            var list = generator.GenerateBricks();
            Assert.IsTrue(list.Count == 1);
        }

    }
}

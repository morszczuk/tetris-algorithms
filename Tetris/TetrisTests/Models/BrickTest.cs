using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.Models;

namespace TetrisTests.Models
{
    [TestClass]
    public class BrickTest
    {
        [TestMethod]
        public void BinaryRepresentation_IsCorrect()
        {
            var body = new bool[2, 2] { { true, true }, { true, false } };
            var brick = new Brick(body);
            var expected = new uint[2] { 3, 1 };
            Assert.IsTrue(brick.BinaryBody.SequenceEqual(expected));
        }

        [TestMethod]
        public void Width_IsCorrect()
        {
            var body = new bool[3, 2] { { true, true }, { true, false }, { false, false } };
            var brick = new Brick(body);
            Assert.AreEqual(2, brick.Width);
        }

        [TestMethod]
        public void Height_IsCorrect()
        {
            var body = new bool[3, 2] { { true, true }, { true, false }, { false, false } };
            var brick = new Brick(body);
            Assert.AreEqual(3, brick.Height);
        }

        [TestMethod]
        public void TilesCount_IsCorrect()
        {
            var body = new bool[3, 2] { { true, true }, { true, false }, { false, true } };
            var brick = new Brick(body);
            Assert.AreEqual(4, brick.TilesCount);
        }
    }
}

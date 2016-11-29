using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.Models;

namespace TetrisTests.Models
{
    [TestClass]
    public class BrickTypeTest
    {
        [TestMethod]
        public void Constructor_Generates1RotationForSquareBody()
        {
            var body = new bool[2, 2] {{true, true}, {true, true} };
            var brickType = new BrickType(body);
            Assert.AreEqual(1, brickType.AvailableRotations.Count());
        }

        [TestMethod]
        public void Constructor_Generates2RotationForRectangularBody()
        {
            var body = new bool[3, 2] { { true, true }, { true, true }, { true, true } };
            var brickType = new BrickType(body);
            Assert.AreEqual(2, brickType.AvailableRotations.Count());
        }

        [TestMethod]
        public void Constructor_Generates4RotationForAsymetricBody()
        {
            var body = new bool[2, 2] { { true, true }, { true, false } };
            var brickType = new BrickType(body);
            Assert.AreEqual(4, brickType.AvailableRotations.Count());
        }

        [TestMethod]
        public void Rotate90Right_RotatesCorrectly()
        {
            var body = new bool[2, 2] { { true, true }, { true, false } };
            var rotated = BrickType.Rotate90Right(new Brick(body));
            var expected = new Brick(new bool[2, 2] { { true, true }, { false, true } });
            Assert.IsTrue(rotated == expected);
        }
    }
}

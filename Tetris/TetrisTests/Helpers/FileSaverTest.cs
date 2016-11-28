using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.Helpers;
using Tetris.Models;
using TetrisTests.TestHelpers;

namespace TetrisTests.Helpers
{
    [TestClass]
    public class FileSaverTest
    {
        private int _maxBricks = 10;
        private int _wellWidth = 10;
        [TestMethod]
        public void CorrectFileSaveTest()
        {
            List<Brick> bricks = new List<Brick>();
            for (int i = 0; i < _maxBricks; i++)
                bricks.Add(AlgorithmTestHelper.CreateRectangleBrick(i, i));

            var saveString = BricksSaver.SaveToFile(bricks, _wellWidth);

            Assert.IsTrue(!string.IsNullOrWhiteSpace(saveString));

            var firstLine = saveString.Split(' ');
            Assert.IsTrue(firstLine[0] == _wellWidth.ToString());
            Assert.IsTrue(saveString.Length > (_maxBricks * _maxBricks / 2 + _wellWidth) * 2);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InCorrectFileSaveTest()
        {
            var saveString = BricksSaver.SaveToFile(null, _wellWidth);
        }

    }
}

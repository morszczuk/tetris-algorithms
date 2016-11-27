using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.Helpers;

namespace TetrisTests.Helpers
{
    [TestClass]
    public class FileLoaderTest
    {
        private readonly string _correctFile = "10 91\r\n4 3\r\n1 1 1 1\r\n1 0 0 1\r\n1 0 0 1\r\n4 3\r\n1 1 1 1\r\n1 1 0 1\r\n0 0 0 1\r\n4 3\r\n1 1 1 1\r\n0 1 0 1\r\n0 0 0 1\r\n4 3\r\n1 1 1 1\r\n1 0 0 1\r\n0 0 0 1\r\n4 3\r\n1 1 1 1\r\n0 0 1 1\r\n0 0 0 1\r\n4 3\r\n1 1 1 1\r\n0 1 1 1\r\n0 0 0 1\r\n4 3\r\n1 1 1 1\r\n0 0 1 1\r\n0 0 1 1\r\n4 3\r\n1 1 1 1\r\n0 1 1 0\r\n0 0 1 1\r\n4 3\r\n1 1 1 1\r\n1 0 1 1\r\n0 0 0 1\r\n4 3\r\n1 1 1 1\r\n1 1 0 0\r\n0 1 0 0\r\n4 3\r\n1 1 1 1\r\n0 1 0 0\r\n0 1 0 0\r\n4 3\r\n1 1 1 1\r\n0 1 1 0\r\n0 1 0 0\r\n4 3\r\n1 1 1 1\r\n0 1 0 0\r\n0 1 1 0\r\n4 3\r\n1 1 1 1\r\n0 1 0 1\r\n0 1 0 0\r\n4 3\r\n1 1 1 1\r\n0 1 0 1\r\n0 1 1 0\r\n4 3\r\n1 1 1 1\r\n0 1 1 0\r\n0 1 1 0\r\n4 3\r\n1 1 1 1\r\n0 1 0 1\r\n0 1 0 1\r\n4 3\r\n1 1 1 1\r\n1 1 0 1\r\n0 1 0 0\r\n4 3\r\n1 1 1 1\r\n1 1 1 0\r\n0 1 0 0\r\n4 3\r\n1 1 1 1\r\n0 1 1 1\r\n0 1 0 0\r\n4 3\r\n1 1 1 1\r\n0 1 0 0\r\n1 1 1 0\r\n4 3\r\n1 1 1 1\r\n0 1 0 0\r\n1 1 0 0\r\n4 3\r\n1 1 1 1\r\n0 0 0 1\r\n0 0 0 1\r\n4 3\r\n0 0 0 1\r\n1 1 1 1\r\n0 0 0 1\r\n4 3\r\n0 0 1 0\r\n1 1 1 1\r\n0 0 1 0\r\n4 3\r\n0 0 1 1\r\n1 1 1 1\r\n0 0 0 1\r\n4 3\r\n0 0 1 1\r\n1 1 1 1\r\n0 0 1 0\r\n4 3\r\n0 0 1 1\r\n1 1 1 1\r\n0 1 0 0\r\n4 3\r\n0 0 1 1\r\n1 1 1 1\r\n1 0 0 0\r\n4 3\r\n0 1 0 0\r\n1 1 1 1\r\n0 1 1 0\r\n4 3\r\n0 0 0 1\r\n1 1 1 1\r\n0 1 1 0\r\n4 3\r\n0 1 0 1\r\n1 1 1 1\r\n0 1 1 0\r\n4 3\r\n0 1 0 0\r\n1 1 1 1\r\n0 0 1 1\r\n4 3\r\n0 1 1 0\r\n1 1 1 1\r\n0 0 1 1\r\n4 3\r\n0 1 0 1\r\n1 1 1 1\r\n0 1 0 1\r\n4 3\r\n0 1 1 0\r\n1 1 1 1\r\n0 1 1 0\r\n4 3\r\n0 0 1 1\r\n1 1 1 1\r\n0 0 1 1\r\n4 3\r\n1 0 0 1\r\n1 1 1 1\r\n1 0 0 1\r\n4 3\r\n1 0 1 1\r\n1 1 1 1\r\n0 0 0 1\r\n4 3\r\n0 1 1 1\r\n1 1 1 1\r\n0 0 0 1\r\n4 3\r\n1 1 0 1\r\n1 1 1 1\r\n0 0 0 1\r\n4 3\r\n1 0 0 1\r\n1 1 1 1\r\n0 0 0 1\r\n4 3\r\n1 0 0 1\r\n1 1 1 1\r\n0 1 0 1\r\n4 3\r\n1 0 0 1\r\n1 1 1 1\r\n0 0 1 1\r\n4 3\r\n1 1 0 0\r\n1 1 1 1\r\n0 0 1 1\r\n4 3\r\n1 0 0 0\r\n1 1 1 1\r\n0 0 0 1\r\n4 3\r\n0 1 0 0\r\n1 1 1 1\r\n0 0 1 0\r\n4 3\r\n0 1 0 0\r\n1 1 1 1\r\n0 0 0 1\r\n4 3\r\n0 0 1 0\r\n1 1 1 1\r\n0 0 0 1\r\n5 4\r\n1 1 0 0 0\r\n0 1 1 0 0\r\n0 0 1 1 0\r\n0 0 0 1 1\r\n5 4\r\n1 0 0 0 0\r\n1 1 1 1 1\r\n1 0 0 0 0\r\n1 0 0 0 0\r\n5 4\r\n0 1 0 0 0\r\n1 1 1 1 1\r\n1 0 0 0 0\r\n1 0 0 0 0\r\n5 4\r\n0 0 1 0 0\r\n1 1 1 1 1\r\n1 0 0 0 0\r\n1 0 0 0 0\r\n5 4\r\n0 0 0 1 0\r\n1 1 1 1 1\r\n1 0 0 0 0\r\n1 0 0 0 0\r\n5 4\r\n0 0 0 0 1\r\n1 1 1 1 1\r\n1 0 0 0 0\r\n1 0 0 0 0\r\n5 4\r\n1 0 0 0 0\r\n1 1 1 1 1\r\n0 1 0 0 0\r\n0 1 0 0 0\r\n5 4\r\n0 1 0 0 0\r\n1 1 1 1 1\r\n0 1 0 0 0\r\n0 1 0 0 0\r\n5 4\r\n0 0 1 0 0\r\n1 1 1 1 1\r\n0 1 0 0 0\r\n0 1 0 0 0\r\n5 4\r\n0 0 0 1 0\r\n1 1 1 1 1\r\n0 1 0 0 0\r\n0 1 0 0 0\r\n5 4\r\n0 0 0 0 1\r\n1 1 1 1 1\r\n0 1 0 0 0\r\n0 1 0 0 0\r\n5 4\r\n1 0 0 0 0\r\n1 1 1 1 1\r\n0 0 1 0 0\r\n0 0 1 0 0\r\n5 4\r\n0 1 0 0 0\r\n1 1 1 1 1\r\n0 0 1 0 0\r\n0 0 1 0 0\r\n5 4\r\n0 0 1 0 0\r\n1 1 1 1 1\r\n0 0 1 0 0\r\n0 0 1 0 0\r\n5 4\r\n0 0 0 1 0\r\n1 1 1 1 1\r\n0 0 1 0 0\r\n0 0 1 0 0\r\n5 4\r\n0 0 0 0 1\r\n1 1 1 1 1\r\n0 0 1 0 0\r\n0 0 1 0 0\r\n5 4\r\n1 0 0 0 0\r\n1 1 1 1 1\r\n0 0 0 1 0\r\n0 0 0 1 0\r\n5 4\r\n0 1 0 0 0\r\n1 1 1 1 1\r\n0 0 0 1 0\r\n0 0 0 1 0\r\n5 4\r\n0 0 1 0 0\r\n1 1 1 1 1\r\n0 0 0 1 0\r\n0 0 0 1 0\r\n5 4\r\n0 0 0 1 0\r\n1 1 1 1 1\r\n0 0 0 1 0\r\n0 0 0 1 0\r\n5 4\r\n0 0 0 0 1\r\n1 1 1 1 1\r\n0 0 0 1 0\r\n0 0 0 1 0\r\n5 4\r\n1 0 0 0 0\r\n1 1 1 1 1\r\n0 0 0 0 1\r\n0 0 0 0 1\r\n5 4\r\n0 1 0 0 0\r\n1 1 1 1 1\r\n0 0 0 0 1\r\n0 0 0 0 1\r\n5 4\r\n0 0 1 0 0\r\n1 1 1 1 1\r\n0 0 0 0 1\r\n0 0 0 0 1\r\n5 4\r\n0 0 0 1 0\r\n1 1 1 1 1\r\n0 0 0 0 1\r\n0 0 0 0 1\r\n5 4\r\n0 0 0 0 1\r\n1 1 1 1 1\r\n0 0 0 0 1\r\n0 0 0 0 1\r\n5 4\r\n1 1 1 1 1\r\n0 0 0 0 1\r\n0 0 0 0 1\r\n0 0 0 0 1\r\n5 4\r\n1 1 1 1 1\r\n0 0 0 1 0\r\n0 0 0 1 0\r\n0 0 0 1 0\r\n5 4\r\n1 1 1 1 1\r\n0 0 1 0 0\r\n0 0 1 0 0\r\n0 0 1 0 0\r\n5 4\r\n1 1 1 1 1\r\n0 1 0 0 0\r\n0 1 0 0 0\r\n0 1 0 0 0\r\n5 4\r\n1 1 1 1 1\r\n1 0 0 0 0\r\n1 0 0 0 0\r\n1 0 0 0 0\r\n5 4\r\n1 1 1 1 0\r\n0 0 0 1 1\r\n0 0 0 0 1\r\n0 0 0 0 1\r\n5 4\r\n1 1 1 1 0\r\n0 0 0 1 1\r\n0 0 0 1 0\r\n0 0 0 1 0\r\n5 4\r\n0 1 1 1 1\r\n1 1 0 0 0\r\n1 0 0 0 0\r\n1 0 0 0 0\r\n5 4\r\n0 1 1 1 1\r\n1 1 0 0 0\r\n0 1 0 0 0\r\n0 1 0 0 0\r\n5 4\r\n1 1 1 1 0\r\n0 0 0 1 0\r\n0 0 0 1 1\r\n0 0 0 0 1\r\n5 4\r\n1 1 1 1 0\r\n0 0 0 1 0\r\n0 0 0 1 1\r\n0 0 0 1 0\r\n5 4\r\n0 1 1 1 1\r\n0 1 0 0 0\r\n1 1 0 0 0\r\n1 0 0 0 0\r\n5 4\r\n0 1 1 1 1\r\n0 1 0 0 0\r\n1 1 0 0 0\r\n0 1 0 0 0\r\n5 4\r\n1 1 1 1 0\r\n0 0 0 1 0\r\n0 0 0 1 0\r\n0 0 0 1 1\r\n5 4\r\n0 1 1 1 1\r\n0 1 0 0 0\r\n0 1 0 0 0\r\n1 1 0 0 0\r\n5 4\r\n1 1 1 0 0\r\n0 0 1 1 1\r\n0 0 0 0 1\r\n0 0 0 0 1";

        private readonly string _correctFile2 =
            "10 60\n4 5\n0 0 1 1\n0 0 1 0\n0 0 1 0\n1 1 1 0\n0 1 1 0\n4 5\n1 0 0 0\n1 1 1 1\n1 0 0 0\n1 1 0 0\n1 0 0 0\n4 5\n0 0 1 1\n0 0 1 0\n1 1 1 0\n0 1 1 0\n0 0 1 0\n4 5\n0 0 1 1\n1 1 1 0\n0 0 1 0\n0 0 1 1\n0 0 0 1\n4 5\n0 0 0 1\n0 1 1 1\n1 1 1 0\n1 0 0 0\n1 0 0 0\n4 5\n1 0 1 1\n1 1 1 0\n0 1 0 0\n0 1 0 0\n0 1 0 0\n4 5\n0 0 1 0\n0 0 1 1\n0 0 0 1\n1 1 1 1\n0 1 0 0\n4 5\n0 0 0 1\n0 0 1 1\n0 0 1 0\n1 1 1 1\n0 0 1 0\n4 5\n1 0 0 0\n1 1 1 1\n0 1 0 0\n0 1 0 0\n0 1 1 0\n4 5\n0 1 0 0\n1 1 1 0\n0 1 1 0\n0 0 1 1\n0 0 0 1\n4 5\n0 0 1 0\n0 1 1 1\n1 1 1 0\n0 1 0 0\n0 1 0 0\n4 5\n0 0 1 0\n1 1 1 0\n0 0 1 0\n0 1 1 1\n0 0 0 1\n4 5\n1 1 0 0\n0 1 1 1\n1 1 0 0\n1 0 0 0\n1 0 0 0\n4 5\n1 1 0 0\n0 1 0 0\n1 1 0 0\n0 1 1 1\n0 0 0 1\n4 5\n1 0 1 0\n1 1 1 0\n0 0 1 1\n0 0 1 0\n0 0 1 0\n4 5\n0 1 1 0\n0 1 0 0\n1 1 1 1\n1 0 0 0\n1 0 0 0\n4 5\n1 1 0 0\n1 0 0 0\n1 0 0 0\n1 1 1 1\n0 0 0 1\n4 5\n1 0 0 0\n1 0 0 0\n1 1 0 1\n0 1 1 1\n0 0 1 0\n4 5\n1 0 0 0\n1 0 0 0\n1 1 1 1\n1 0 1 0\n1 0 0 0\n4 5\n0 1 0 0\n1 1 0 0\n1 1 1 0\n0 0 1 1\n0 0 1 0\n4 5\n1 0 0 0\n1 0 0 0\n1 1 1 0\n0 1 1 1\n0 1 0 0\n4 5\n1 1 1 0\n0 1 1 0\n0 0 1 1\n0 0 1 0\n0 0 1 0\n4 5\n0 0 1 0\n1 1 1 1\n0 1 1 0\n0 0 1 0\n0 0 1 0\n4 5\n0 0 0 1\n0 0 0 1\n1 1 1 1\n1 1 0 0\n0 1 0 0\n4 5\n0 1 1 1\n0 1 0 0\n1 1 1 0\n0 1 0 0\n0 1 0 0\n4 5\n0 0 0 1\n0 0 0 1\n0 1 1 1\n1 1 0 0\n1 1 0 0\n4 5\n0 1 0 0\n0 1 0 0\n0 1 0 1\n1 1 1 1\n0 1 0 0\n4 5\n0 1 0 0\n1 1 0 0\n1 1 0 0\n0 1 1 0\n0 0 1 1\n4 5\n0 1 0 0\n0 1 1 1\n0 1 0 0\n0 1 0 0\n1 1 1 0\n4 5\n0 1 0 0\n0 1 0 0\n0 1 0 1\n1 1 1 1\n0 0 1 0\n4 5\n0 0 1 1\n0 0 0 1\n0 0 1 1\n1 1 1 0\n0 0 1 0\n4 5\n0 0 0 1\n0 0 0 1\n1 1 1 1\n1 1 0 0\n1 0 0 0\n4 5\n1 0 0 1\n1 1 1 1\n0 0 1 0\n0 0 1 0\n0 0 1 0\n4 5\n0 0 0 1\n1 1 1 1\n0 0 1 0\n0 0 1 1\n0 0 1 0\n4 5\n0 0 1 1\n0 0 0 1\n0 0 1 1\n0 0 1 0\n1 1 1 0\n4 5\n0 1 0 0\n0 1 1 0\n0 1 0 0\n1 1 0 0\n0 1 1 1\n4 5\n1 1 1 0\n0 1 0 0\n0 1 1 1\n0 0 1 0\n0 0 1 0\n4 5\n0 0 1 0\n0 1 1 1\n0 0 1 0\n1 1 1 0\n0 0 1 0\n4 5\n0 0 0 1\n0 0 0 1\n0 1 0 1\n1 1 1 1\n0 0 1 0\n4 5\n0 1 0 0\n0 1 0 0\n1 1 1 0\n1 0 1 0\n0 0 1 1\n4 5\n0 0 0 1\n0 0 0 1\n0 1 1 1\n1 1 0 1\n0 0 0 1\n4 5\n1 0 0 0\n1 1 0 0\n0 1 1 0\n0 0 1 1\n0 1 1 0\n4 5\n0 1 1 0\n0 1 0 0\n1 1 0 0\n0 1 1 1\n0 0 0 1\n4 5\n0 0 1 0\n0 0 1 0\n1 1 1 1\n1 0 0 0\n1 1 0 0\n4 5\n0 0 1 0\n0 0 1 1\n1 1 1 1\n0 1 0 0\n0 1 0 0\n4 5\n0 0 0 1\n1 1 1 1\n0 0 1 1\n0 0 0 1\n0 0 0 1\n4 5\n1 1 0 0\n1 1 0 0\n0 1 1 1\n0 0 0 1\n0 0 0 1\n4 5\n0 0 1 0\n0 0 1 1\n1 0 1 0\n1 1 1 0\n0 1 0 0\n4 5\n0 0 0 1\n0 0 1 1\n1 1 1 0\n0 0 1 1\n0 0 1 0\n4 5\n0 0 1 0\n0 1 1 0\n0 1 1 1\n1 1 0 0\n1 0 0 0\n4 5\n0 0 0 1\n1 1 1 1\n1 0 0 0\n1 0 0 0\n1 1 0 0\n4 5\n0 1 0 0\n1 1 0 0\n0 1 0 0\n1 1 1 1\n0 1 0 0\n4 5\n1 1 0 0\n0 1 0 0\n1 1 1 1\n0 0 1 0\n0 0 1 0\n4 5\n0 1 1 1\n0 1 0 1\n0 1 0 0\n0 1 0 0\n1 1 0 0\n4 5\n0 0 1 0\n0 1 1 0\n0 0 1 1\n1 1 1 0\n0 0 1 0\n4 5\n0 0 1 1\n0 0 0 1\n0 0 0 1\n1 1 1 1\n0 0 1 0\n4 5\n1 0 0 0\n1 1 1 0\n0 0 1 1\n0 1 1 0\n0 1 0 0\n4 5\n1 0 0 0\n1 1 0 0\n1 1 1 1\n0 0 1 0\n0 0 1 0\n4 5\n0 0 1 1\n0 0 1 0\n0 0 1 0\n1 1 1 1\n0 0 1 0\n4 5\n0 0 1 0\n0 1 1 0\n1 1 0 0\n0 1 1 1\n0 0 1 0\n";
        [TestMethod]
        public void CorrectFileLoadedTest()
        {
            var exampleStream = new MemoryStream(Encoding.UTF8.GetBytes(_correctFile));

            BricksLoader loader = new BricksLoader(new StreamReader(exampleStream));
            var result = loader.ReadFile();

            Assert.IsTrue(result.BrickTypes != null);
            Assert.IsTrue(result.BricksNumber == 91);
            Assert.IsTrue(result.WellWidth == 10);
        }

        [TestMethod]
        public void OurFileLoadedTest()
        {
            var exampleStream = new MemoryStream(Encoding.UTF8.GetBytes(_correctFile2));

            BricksLoader loader = new BricksLoader(new StreamReader(exampleStream));
            var result = loader.ReadFile();

            Assert.IsTrue(result.BrickTypes != null);
            Assert.IsTrue(result.BricksNumber == 60);
            Assert.IsTrue(result.WellWidth == 10);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void InCorrectFileLoadedTest()
        {
            var exampleStream = new MemoryStream(Encoding.UTF8.GetBytes("random incorrect string"));
            BricksLoader loader = new BricksLoader(new StreamReader(exampleStream));
            var result = loader.ReadFile();
        }

    }
}

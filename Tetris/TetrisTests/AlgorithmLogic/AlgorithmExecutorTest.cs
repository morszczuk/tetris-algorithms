using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.AlgorithmLogic;
using Tetris.AlgorithmLogic.Evaluators;
using Tetris.Enums;
using TetrisTests.TestHelpers;

namespace TetrisTests.AlgorithmLogic
{
    [TestClass]
    public class AlgorithmExecutorTest
    {
        [TestMethod]
        public void IsFinished_ForStateWithoutBricks_ReturnsTrue()
        {
            var bricksShelf = AlgorithmTestHelper.CreateBricksShelfWithNRectangleBricks(0);
            var settings = new AlgorithmInput(bricksShelf, 2, 10, AlgorithmsEnum.Continuous, typeof(AlgorithmTestHelper.MockEvaluator), true);
            var algorithm = new AlgorithmExecutor(settings, new AlgorithmTestHelper.MockEvaluator(), new AlgorithmTestHelper.MockPositioner());
            Assert.IsTrue(algorithm.IsFinished());
        }

        [TestMethod]
        public void IsFinished_ForStateWithBricks_ReturnsFalse()
        {
            var bricksShelf = AlgorithmTestHelper.CreateBricksShelfWithNRectangleBricks(1);
            var settings = new AlgorithmInput(bricksShelf, 2, 10, AlgorithmsEnum.Continuous, typeof(AlgorithmTestHelper.MockEvaluator), true);
            var algorithm = new AlgorithmExecutor(settings, new AlgorithmTestHelper.MockEvaluator(), new AlgorithmTestHelper.MockPositioner());
            Assert.IsFalse(algorithm.IsFinished());
        }

        [TestMethod]
        public void MakeStep_For3Wells_Return3Wells()
        {
            var bricksShelf = AlgorithmTestHelper.CreateBricksShelfWithNRectangleBricks(2);
            var settings = new AlgorithmInput(bricksShelf, 3, 10, AlgorithmsEnum.Continuous, typeof(AlgorithmTestHelper.MockEvaluator), true);
            var algorithm = new AlgorithmExecutor(settings, new AlgorithmTestHelper.MockEvaluator(), new AlgorithmTestHelper.MockPositioner());
            algorithm.MakeStep();
            Assert.AreEqual(3, algorithm.ActiveStates.Count);
        }

        [TestMethod]
        public void MakeStep_WithBricks_ReturnWellsWithOneBrickPlaced()
        {
            var bricksShelf = AlgorithmTestHelper.CreateBricksShelfWithNRectangleBricks(3);
            var settings = new AlgorithmInput(bricksShelf, 2, 10, AlgorithmsEnum.Continuous, typeof(AlgorithmTestHelper.MockEvaluator), true);
            var algorithm = new AlgorithmExecutor(settings, new AlgorithmTestHelper.MockEvaluator(), new AlgorithmTestHelper.MockPositioner());
            algorithm.MakeStep();
            Assert.IsTrue(algorithm.ActiveStates.All(s => s.Bricks.Count == 1));
            Assert.IsTrue(algorithm.ActiveStates.All(s => s.BricksShelf.AvailableBrickTypes.Count() == 2));
        }

        [TestMethod]
        public void MakeStep_With2Bricks_FinishesAfterTwoIterations()
        {
            var bricksShelf = AlgorithmTestHelper.CreateBricksShelfWithNRectangleBricks(2);
            var settings = new AlgorithmInput(bricksShelf, 3, 10, AlgorithmsEnum.Continuous, typeof(AlgorithmTestHelper.MockEvaluator), true);
            var algorithm = new AlgorithmExecutor(settings, new AlgorithmTestHelper.MockEvaluator(), new AlgorithmTestHelper.MockPositioner());
            Assert.IsFalse(algorithm.IsFinished());
            algorithm.MakeStep();
            Assert.IsFalse(algorithm.IsFinished());
            algorithm.MakeStep();
            Assert.IsTrue(algorithm.IsFinished());
        }

    }
}

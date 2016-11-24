using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.AlgorithmLogic.Evaluators;
using TetrisTests.TestHelpers;

namespace TetrisTests.AlgorithmLogic.Evaluators
{
    [TestClass]
    public class PointEvaluatorTest
    {

        [TestMethod]
        public void Evaluate_WithEmptyWellState_ReturnsZero()
        {
            var evaluator = new PointEvaluator();
            var emptyWellState = AlgorithmTestHelper.CreateEmptyWellState(10);
            Assert.AreEqual(0, evaluator.Evaluate(emptyWellState));
        }

        [TestMethod]
        public void Evaluate_WithOneLongBrick()
        {
            var evaluator = new PointEvaluator();
            var fullWellState = AlgorithmTestHelper.CreateWellStateWithOneBrick(10, 1);
            Assert.AreEqual(12*evaluator._startWallPoint*evaluator._scaleConst, evaluator.Evaluate(fullWellState));
        }

        [TestMethod]
        public void Evaluate_WithTwoLongBricks()
        {
            var evaluator = new PointEvaluator();
            var fullWellState = AlgorithmTestHelper.CreateWellStateWithNBricks(10, 1,2);

            var points = ((2* evaluator._startWallPoint) + (10* evaluator._startNeightBourPoint))* evaluator._scaleConst;
            Assert.AreEqual(points, evaluator.Evaluate(fullWellState));
        }

    }
}

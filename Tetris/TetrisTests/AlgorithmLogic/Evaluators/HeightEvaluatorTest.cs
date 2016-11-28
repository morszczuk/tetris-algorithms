using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.AlgorithmLogic.Evaluators;
using TetrisTests.TestHelpers;

namespace TetrisTests.AlgorithmLogic.Evaluators
{
    [TestClass]
    public class HeightEvaluatorTest
    {
        [TestMethod]
        public void Evaluate_WithEmptyWellState_ReturnsZero()
        {
            var evaluator = new HeightEvaluator();
            var emptyWellState = AlgorithmTestHelper.CreateEmptyWellState(10);
            Assert.AreEqual(0, evaluator.Evaluate(emptyWellState));
        }

        [TestMethod]
        public void Evaluate_WithFullWell_ReturnsHeight()
        {
            var evaluator = new HeightEvaluator();
            var fullWellState = AlgorithmTestHelper.CreateFullWellState(10, 5);
            Assert.AreEqual(-5, evaluator.Evaluate(fullWellState));
        }

        [TestMethod]
        public void Evaluate_WithUnequalFill_ReturnsHeight()
        {
            var evaluator = new HeightEvaluator();
            var fullWellState = AlgorithmTestHelper.CreateFullWellState(10, 4);
            fullWellState.AddBrick(AlgorithmTestHelper.CreateRectangleBrick(5, 2), 0, 4);
            fullWellState.AddBrick(AlgorithmTestHelper.CreateRectangleBrick(5, 2), 2, 6);
            Assert.AreEqual(-8, evaluator.Evaluate(fullWellState));
        }
    }
}

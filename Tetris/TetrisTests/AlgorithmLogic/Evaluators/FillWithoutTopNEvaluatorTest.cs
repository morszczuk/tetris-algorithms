using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.AlgorithmLogic.Evaluators;
using TetrisTests.TestHelpers;

namespace TetrisTests.AlgorithmLogic.Evaluators
{
    [TestClass]
    public class FillWithoutTopNEvaluatorTest
    {
        [TestMethod]
        public void Evaluate_WithEmptyWellState_ReturnsZero()
        {
            var evaluator = new FillWithoutTopNEvaluator(2);
            var emptyWellState = AlgorithmTestHelper.CreateEmptyWellState(10);
            Assert.AreEqual(0, evaluator.Evaluate(emptyWellState));
        }

        [TestMethod]
        public void Evaluate_WithFullWell_ReturnsHeight()
        {
            var evaluator = new FillWithoutTopNEvaluator(2);
            var fullWellState = AlgorithmTestHelper.CreateFullWellState(10, 5);
            Assert.AreEqual(10000, evaluator.Evaluate(fullWellState));
        }

        [TestMethod]
        public void Evaluate_WithUnequalFill_ReturnsHeight()
        {
            var evaluator = new FillWithoutTopNEvaluator(1);
            var fullWellState = AlgorithmTestHelper.CreateFullWellState(10, 4);
            fullWellState.AddBrick(AlgorithmTestHelper.CreateRectangleBrick(5, 2), 0, 4);
            fullWellState.AddBrick(AlgorithmTestHelper.CreateRectangleBrick(5, 2), 2, 6);
            Assert.AreEqual(7143, evaluator.Evaluate(fullWellState));
        }
    }
}

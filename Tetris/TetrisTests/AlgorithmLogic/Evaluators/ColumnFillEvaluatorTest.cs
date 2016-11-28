using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.AlgorithmLogic.Evaluators;
using TetrisTests.TestHelpers;

namespace TetrisTests.AlgorithmLogic.Evaluators
{
    [TestClass]
    public class ColumnFillEvaluatorTest
    {
        [TestMethod]
        public void Evaluate_WithEmptyWellState_ReturnsZero()
        {
            var evaluator = new ColumnFillEvaluator();
            var emptyWellState = AlgorithmTestHelper.CreateEmptyWellState(10);
            Assert.AreEqual(0, evaluator.Evaluate(emptyWellState));
        }

        [TestMethod]
        public void Evaluate_WithFullWell_ReturnsDoubleFill()
        {
            var evaluator = new ColumnFillEvaluator();
            var fullWellState = AlgorithmTestHelper.CreateFullWellState(10, 5);
            Assert.AreEqual(-(50 + 40), evaluator.Evaluate(fullWellState));
        }

        [TestMethod]
        public void Evaluate_WithUnequalFill_ReturnsValidResult()
        {
            var evaluator = new ColumnFillEvaluator();
            var fullWellState = AlgorithmTestHelper.CreateFullWellState(10, 4);
            fullWellState.AddBrick(AlgorithmTestHelper.CreateRectangleBrick(5, 2), 0, 4);
            fullWellState.AddBrick(AlgorithmTestHelper.CreateRectangleBrick(5, 2), 2, 6);
            Assert.AreEqual(-(80 + 6 * 7 + 4 * 3), evaluator.Evaluate(fullWellState));
        }
    }
}

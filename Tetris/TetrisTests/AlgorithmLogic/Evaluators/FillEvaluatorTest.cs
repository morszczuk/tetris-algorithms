using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.AlgorithmLogic.Evaluators;
using Tetris.Models;
using TetrisTests.TestHelpers;

namespace TetrisTests.AlgorithmLogic.Evaluators
{
    [TestClass]
    public class FillEvaluatorTest
    {

        [TestMethod]
        public void Evaluate_WithEmptyWellState_ReturnsZero()
        {
            var evaluator = new FillEvaluator();
            var emptyWellState = AlgorithmTestHelper.CreateEmptyWellState(10);
            Assert.AreEqual(0, evaluator.Evaluate(emptyWellState));
        }

        [TestMethod]
        public void Evaluate_WithFullWell_ReturnsMaxValue()
        {
            var evaluator = new FillEvaluator();
            var fullWellState = AlgorithmTestHelper.CreateFullWellState(10, 5);
            Assert.AreEqual(FillEvaluator.MaxValue, evaluator.Evaluate(fullWellState));
        }

        [TestMethod]
        public void Evaluate_WithUnequalFill_ReturnsValidResult()
        {
            var evaluator = new FillEvaluator();
            var fullWellState = AlgorithmTestHelper.CreateFullWellState(10, 4);
            fullWellState.AddBrick(AlgorithmTestHelper.CreateRectangleBrick(5, 2), 0, 4);
            fullWellState.AddBrick(AlgorithmTestHelper.CreateRectangleBrick(5, 2), 2, 6);
            Assert.AreEqual(0.75 * FillEvaluator.MaxValue, evaluator.Evaluate(fullWellState));
        }
    }
}

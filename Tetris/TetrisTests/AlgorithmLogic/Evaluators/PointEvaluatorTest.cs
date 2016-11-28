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
            Assert.AreEqual(12 * PointEvaluator.StartWallPoint * PointEvaluator.ScaleConst, evaluator.Evaluate(fullWellState));
        }

        [TestMethod]
        public void Evaluate_WithTwoLongBricks()
        {
            var evaluator = new PointEvaluator();
            var fullWellState = AlgorithmTestHelper.CreateWellStateWithNBricks(10, 1, 2);

            var points = ((2 * PointEvaluator.StartWallPoint) + (10 * PointEvaluator.StartNeightBourPoint)) * PointEvaluator.ScaleConst;
            Assert.AreEqual(points, evaluator.Evaluate(fullWellState));
        }

        [TestMethod]
        public void Evaluate_WithThreeLongBricks()
        {
            var evaluator = new PointEvaluator();
            var fullWellState = AlgorithmTestHelper.CreateWellStateWithNBricks(10, 1, 3);

            var points = ((1 * PointEvaluator.StartWallPoint) + (10 * PointEvaluator.StartNeightBourPoint)) * PointEvaluator.ScaleConst;
            Assert.AreEqual(points, evaluator.Evaluate(fullWellState));
        }
        [TestMethod]
        public void Evaluate_WithFourLongBricks()
        {
            var evaluator = new PointEvaluator();
            var fullWellState = AlgorithmTestHelper.CreateWellStateWithNBricks(10, 1, 4);
            var wallPoints =  ((double)2/(double)3)*(double)PointEvaluator.StartWallPoint;
            var neighPoints = (10/2*(double)PointEvaluator.StartNeightBourPoint);
            var points = (wallPoints+ neighPoints) * PointEvaluator.ScaleConst;
            Assert.AreEqual(points, evaluator.Evaluate(fullWellState));
        }

        [TestMethod]
        public void Evaluate_WithFiveLongBricks()
        {
            var evaluator = new PointEvaluator();
            var fullWellState = AlgorithmTestHelper.CreateWellStateWithNBricks(10, 1, 5);
            var wallPoints = ((double)2 / (double)4) * (double)PointEvaluator.StartWallPoint;
            var neighPoints = ((double)10/(double)3 * (double)PointEvaluator.StartNeightBourPoint);
            var points = (int)((wallPoints + neighPoints) * PointEvaluator.ScaleConst);
            Assert.AreEqual(points, evaluator.Evaluate(fullWellState));
        }
    }
}

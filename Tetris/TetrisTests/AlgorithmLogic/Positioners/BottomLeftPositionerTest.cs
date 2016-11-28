using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris.AlgorithmLogic.Positioners;
using Tetris.Models;
using TetrisTests.TestHelpers;

namespace TetrisTests.AlgorithmLogic.Positioners
{
    [TestClass]
    public class BottomLeftPositionerTest
    {

        [TestMethod]
        public void BasicBottomLeftPositionerTest()
        {
            int bricksNo = 100;
            int wellWidth = 10;

            var wellState = AlgorithmTestHelper.CreateEmptyWellState(wellWidth);
            var positioner = new BasicBottomLeftPositioner();

            List<Brick> bricks = new List<Brick>();
            for (int i = 0; i < bricksNo; i++)
                bricks.Add(AlgorithmTestHelper.CreateRectangleBrick(1, 1));

            int row = 0;
            for (int i = 0; i < bricks.Count; i++)
            {
                if (i == wellWidth) row++;
                var state = positioner.PlaceBrick(wellState, bricks[i]);
                Assert.IsTrue(state.First().IsFilled(i, row));
            }
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BasicBottomLeftPositionerBrickNullTest()
        {
            var wellState = AlgorithmTestHelper.CreateEmptyWellState(10);
            var positioner = new BasicBottomLeftPositioner();
            var state = positioner.PlaceBrick(wellState, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BasicBottomLeftPositionerWellStateNullTest()
        {
            var positioner = new BasicBottomLeftPositioner();
            var state = positioner.PlaceBrick(null, null);
        }

    }
}

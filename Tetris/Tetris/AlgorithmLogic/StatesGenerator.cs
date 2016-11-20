using System;
using System.Collections.Generic;
using System.Linq;
using Tetris.AlgorithmLogic.Evaluators;
using Tetris.AlgorithmLogic.Positioners;
using Tetris.AlgorithmLogic.Strategies;
using Tetris.Models;

namespace Tetris.AlgorithmLogic
{
    public class StatesGenerator
    {

        private readonly IBrickPositioner _positioner;

        public StatesGenerator(IBrickPositioner positioner)
        {
            _positioner = positioner;
        }

        public List<WellState> Generate(WellState wellState)
        {
            var newStates = new List<WellState>();
            foreach (var brick in wellState.BricksShelf.AvailableBricks())
            {
                var tmpState = new WellState(wellState);
                tmpState.BricksShelf.Bricks[brick]--;
                newStates.AddRange(_positioner.PlaceBrick(tmpState, brick));
            }
            return newStates;
        }

    }
}
